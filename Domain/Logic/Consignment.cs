namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.SymbolStore;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using APIHandler;
    using Domain.AEB.DTOs;
    using Domain.Exceptions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;

    public partial class Consignment : ILoggable
    {
        static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly DateTime Date = LocalTime.Now;
        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            await Validation();
            if (IsHaveAllDeclaraionDetails() && GetNumberOfDecimalPlaces((decimal)TotalGrossWeight) > 2)
                throw new ValidationException("Total Gross Weight should only have 2 decimal places.");

            if (IsHaveAllDeclaraionDetails() && GetNumberOfDecimalPlaces((decimal)TotalNetWeight) > 3)
                throw new ValidationException("Total Net Weight should only have 3 decimal places.");

            if (IsHaveAllDeclaraionDetails() && TotalValue.HasValue && GetNumberOfDecimalPlaces(TotalValue.Value) > 2)
                throw new ValidationException("Total Value should only have 2 decimal places.");

            if (TotalNetWeight > TotalGrossWeight)
                throw new ValidationException("Total Net Weight cannot be greater than Total Gross Weight.");

            if (UKTrader.EORINumber == Declarant.EORINumber)
            {
                Declarant = await Company.ChannelPort;
            }

            if (!IsImporterPayingTheFreight)
            {
                FreightAmount = null;
                FreightCurrency = null;
            }
            if (!IsNew && TotalGrossWeight < await Commodities.Sum(x => x.GrossWeight))
                throw new ValidationException("The Consignment Gross Weight total must be equal to the Gross Weights of the Commodities");

            if (!IsNew && TotalNetWeight < await Commodities.Sum(x => x.NetWeight))
                throw new ValidationException("The Consignment net Weight total must be equal to the net Weights of the Commodities");

            if (CFSPShipmentNumber.HasValue())
                if (await Database.Of<Consignment>().Any(x => x.CFSPShipmentNumber == CFSPShipmentNumber && x.ID != ID && x.Shipment.IsAPI == false))
                    throw new ValidationException("CFSP Shipment Number must be unique");

            if (IntoUKTypeId == PortType.GVMS)
                UCN = null;
        }

        public async Task Validation()
        {
            var result = new List<string>();

            if (IsHaveAllDeclaraionDetails() && TotalGrossWeight < 0.01)
                result.Add("The value of Total gross weight must be 0.01 or more.");

            if (IsHaveAllDeclaraionDetails() && TotalGrossWeight > 99999999999.99)
                result.Add("The value of Total gross weight must be 99999999999.99 or less.");

            if (IsHaveAllDeclaraionDetails() && TotalNetWeight < 0.01)
                result.Add("The value of Total net weight must be 0.01 or more.");

            if (IsHaveAllDeclaraionDetails() && TotalNetWeight > 99999999999.99)
                result.Add("The value of Total net weight must be 99999999999.99 or less.");

            if (IsHaveAllDeclaraionDetails() && InvoiceCurrencyId == null)
                result.Add("Please provide a value for Invoice currency.");

            if (IsHaveAllDeclaraionDetails() && TotalValue < 0.01m)
                result.Add("The value of Total value must be 0.01 or more.");

            if (IsHaveAllDeclaraionDetails() && TotalValue > 9999999999.99m)
                result.Add("The value of Total value must be 9999999999.99 or less.");

            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }

        public async Task ValidateCompletion()
        {
            var total = await Commodities.Sum(x => x.Value);
            if (TotalValue != total)
                throw new ValidationException($"The total value for consignment is £{TotalValue} and the " +
                    $"total value for the commodities within this consignment is £{total}. These values do not match.");

            if (!(await IsWeightMatch()))
                throw new ValidationException("The gross and net weights for Commodities must either be all completed or all blank.");

            var totalNet = await Commodities.Sum(x => x.NetWeight);
            if (TotalNetWeight != totalNet && totalNet > 0)
                throw new ValidationException($"The total net weight for consignment is {TotalNetWeight} kg and the " +
                    $"total net weight for the commodities within this consignment is {totalNet} kg. These values do not match.");

            var totalGross = await Commodities.Sum(x => x.GrossWeight);
            if (TotalGrossWeight != totalGross && totalGross > 0)
                throw new ValidationException($"The total gross weight for consignment is {TotalGrossWeight} kg and the " +
                    $"total gross weight for the commodities within this consignment is {totalGross} kg. These values do not match.");

            var totalPackages = (await Commodities.GetList()).Sum(x => x.NumberOfPackages);
            if (TotalPackages != totalPackages && totalPackages > 0)
                throw new ValidationException($"The total packages for consignment is {TotalPackages} and the " +
                    $"total packages for the commodities within this consignment is {totalPackages}. These values do not match.");
        }

        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            await Shipment.Company.AddCompaniesToAssociatedCompanies(this);
        }

        public int GetNumberOfDecimalPlaces(decimal number) => BitConverter.GetBytes(decimal.GetBits(number)[3])[2];

        public async Task<int> GenerateIdNumber(Shipment shipment)
        {
            var maxCons = await shipment.Consignments.WithMax(t => t.IdNumber);
            return maxCons?.IdNumber + 1 ?? 1;
        }

        public async Task<string> GenerateConsignmentNumber(Shipment shipment)
        {
            return $"{shipment.TrackingNumber}{(await GenerateIdNumber(shipment)).ToString().PadLeft(2, '0')}";
        }

        public async Task FillCommodityWeights()
        {
            if (IsHaveAllDeclaraionDetails())
            {
                foreach (var commodity in await Commodities.GetList())
                {
                    var tempcommodity = await Database.Reload(commodity);
                    await Database.Update(tempcommodity, x => x.NetWeight = (double)(x.Value / TotalValue.Value) * TotalNetWeight.Value);
                    tempcommodity = await Database.Reload(tempcommodity);
                    await Database.Update(tempcommodity, x => x.GrossWeight = (double)(x.Value / TotalValue.Value) * TotalGrossWeight.Value);
                }
            }

        }

        public async Task<IEnumerable<Product>> GetUkTraderOrCompanyProducts(bool orderResult = false)
        {
            var result = new List<Product>();

            //if (UKTraderId.HasValue)
            //{
            result.AddRange((await UKTrader.Products
                                    .Where(x => !x.IsDeactivated && !x.IsCreatedFromAPI)
                                    .GetList())
                                    .GroupBy(x => x.Code)
                                    .Select(x => x.FirstOrDefault())
                                    .ToList());
            //}
            //else
            //{
            result.AddRange((await Shipment.Company.Products
                                            .Where(x => !x.IsDeactivated && !x.IsCreatedFromAPI)
                                            .GetList())
                                            .GroupBy(x => x.Code)
                                            .Select(x => x.FirstOrDefault())
                                            .ToList());
            //}

            if (orderResult)
                result = result.Distinct().OrderBy(x => x.Name).ToList();

            return result;
        }

        internal ConsignmentDTO ToDto()
        {
            return new ConsignmentDTO
            {
                ConsignmentIdClientSystem = ConsignmentNumber,
                OrganizationUnitClientSystem = Config.Get<string>("AEBCustomsManagement:ClientName"),
                ConsignmentNumber = ConsignmentNumber,
                Remark = "Export from ChannelPorts in house system",
                ProfileCode = "STD",
                PersonInCharge = Shipment.PrimaryContact.ToDTO(),
                Deliveries = Commodities.GetList().GetAwaiter().GetResult().Select(c => c.ToDTO()).ToList(),
                //Costs = new List<CostDTO> { },
            };
        }

        private async Task<bool> IsWeightMatch()
        {
            var commodities = await Commodities.GetList();
            return commodities.All(x => x.GrossWeight == 0 && x.NetWeight == 0) || commodities.All(x => x.GrossWeight != 0 && x.NetWeight != 0);
        }

        public Task<string> DownloadJson()
        {
            return new AEBIntegrationService(Database).Process(this.Shipment);
        }

        public static Task<Consignment> FindByEADMRN(string eadmrn)
        {
            return Database.FirstOrDefault<Consignment>(c => c.EADMRN == eadmrn);
        }

        public async Task<double> GetCurrentGrossWeight()
        => (await Commodities.GetList()).Except(c => !c.GrossWeight.HasValue).Sum(c => c.GrossWeight.Value);

        public async Task<double> GetCurrentNetWeight()
        => (await Commodities.GetList()).Except(c => !c.NetWeight.HasValue).Sum(c => c.NetWeight.Value);

        public async Task<int> GetCurrentNumberOfPackages()
        => (await Commodities.GetList()).Except(c => !c.NumberOfPackages.HasValue).Sum(c => c.NumberOfPackages ?? 0);

        public async Task<decimal> GetCurrentValue()
        => (await Commodities.GetList()).Except(c => !c.Value.HasValue).Sum(c => c.Value.Value);

        public string CreateUCR(string eoriNo, string consNo) => $"{LocalTime.Now.Year.ToString().Substring(3, 1)}{eoriNo}-{consNo}";

        public string CreateDUCR(string authorisationnumber, string sequencenumber) => $"{LocalTime.Now.Year.ToString().Substring(3, 1)}{authorisationnumber}-{sequencenumber}";


        public bool IsReadyToTransmit => ProgressId == Progress.ReadyToTransmit;

        public bool IsEditable(User user)
        {
            if (Progress.IsAnyOf(Helper.PublicEditableConsignments))
                return true;

            if ((user as ChannelPortsUser) != null)
                return ProgressId.IsAnyOf(Helper.AdminEditableConsignments) || (ProgressId == Progress.ASMAccept && this.Shipment.IsInUK);
            return false;
        }

        public bool CanBeDeleted => ProgressId.IsAnyOf(Helper.Removable);
        public async Task<bool> CanAddCommodity(User user) => IsEditable(user) && (!Only1Commodity || (await Commodities.None()));

        public bool CanBeTransmit => ProgressId.IsAnyOf(Helper.Transmitables);

        public bool CanBeFlagedAsArrived => ProgressId == Progress.QueriedArrived;

        public bool CanBeFlagedAsWithCustom => ProgressId == Progress.QueriedWithCustoms;

        public bool CanBeFlagedAsCleared => ProgressId == Progress.WithCustoms && Shipment.IsInUK;

        public bool CanSendToCustom => !Progress.IsManual;

        public async Task<bool> CanBeDownload() => await Documents.Any();

        public string GetCustomerStatusLabel()
            => Progress.ClientDisplay + (Shipment.IsAPI ? "(API)" : "");

        public string GetAdminStatusLabel()
            => Progress.AdminDisplay + (Shipment.IsAPI ? "(API)" : "");

        public async Task<Progress> GetNextStatus()
        {
            //Manual - General
            if (Shipment.IsInUK && (await this.IsInventoryPort(Shipment.Route?.UKPort) && this.IntoUKTypeId == PortType.Inventory) && UCN.IsEmpty())
                return Progress.ManualGenereal;

            //if (Shipment.IsOutUK && Shipment.Route?.UKPort?.OutOfUKType == PortType.Inventory)
            //    return Progress.ManualGenereal;

            var zType = await VATType.FindByName("Z");
            var sType = await VATType.FindByName("S");

            if (await Commodities.Any(x => x.Product.VATId != zType && x.Product.VATId != sType
                    && x.VATId != zType && x.VATId != sType))
                return Progress.ManualGenereal;

            //Manual - CPC

            if (UseSpecialCPC && SpecialCPC?.Manual == true)
                return Progress.ManualCPC;

            if (!UseSpecialCPC)
            {
                var commoditiesList = await Commodities.GetList();

                if (Shipment.IsInUK && commoditiesList.Any(x => x.HasPreference == true && x.CountryOfDestination.ImportCPCWithPreference?.Manual == true))
                    return Progress.ManualCPC;

                if (Shipment.IsInUK && commoditiesList.Any(x => x.HasPreference == false && x.CountryOfDestination.ImportCPCWithoutPreference?.Manual == true))
                    return Progress.ManualCPC;

                if (!Shipment.IsInUK && commoditiesList.Any(x => x.HasPreference == true && x.CountryOfDestination.ExportCPCWithPreference?.Manual == true))
                    return Progress.ManualCPC;

                if (!Shipment.IsInUK && commoditiesList.Any(x => x.HasPreference == false && x.CountryOfDestination.ExportCPCWithoutPreference?.Manual == true))
                    return Progress.ManualCPC;
            }

            if (Shipment.IsInUK && await Commodities.Any(c => c.Product.CommodityCode.OtherQuota == true))
                return Progress.ManualQuota;

            //Manual - License
            if (await Commodities.Any(c => c.GoodsLicencable))
                return Progress.ManualLicense;

            return Progress.ReadyToTransmit;
        }

        public async Task TriggerCommodityControlEmail()
        {
            var hasControl = await Commodities.Any(x => x.Product.CommodityCode.Control);

            if (hasControl)
                await EmailTemplate.CommodityCodeControlEmail.Send(AppSetting.CustomsProEmail,
              new
              {
                  UKTRADER = UKTrader.Name,
                  CONSIGNMENTTRACKINGNUMBER = ConsignmentNumber
              });
        }

        #region Flags
        public async Task ResetStatus()
        {
            await FlagAsDraft();
        }

        public async Task FlagAsCompleted()
        {
            await ValidateCompletion();

            await Database.EnlistOrCreateTransaction(async () =>
            {
                if (await Commodities.Any(x => x.NetWeight < 0))
                    await FillCommodityWeights();

                var status = await GetNextStatus();

                var ucr = string.Empty;
                var originalUCR = UCR;
                var isCFSPUCRUpdated = CFSPUCRUpdated;

                if (!CFSPUCRUpdated)
                {
                    if (UKTraderId.HasValue && UKTrader.CFSPTypeId == CFSPType.Channelports)
                    {
                        //var currentState = Settings.Current;
                        //await Database.Update(currentState, x => x.CFSPShipmentNumber = currentState.CFSPShipmentNumber + 1 ?? 0);
                        ucr = CreateUCR(Declarant.EORINumber, Settings.Current.ChannelportsCFSPShipmentNumber);
                        isCFSPUCRUpdated = true;
                    }
                    else if (UKTraderId.HasValue && UKTrader.CFSPTypeId == CFSPType.Own && Shipment.IsInUK && UseEIDR && SequenceNumber.HasValue())
                    {
                        ucr = CreateDUCR(Shipment.Company.AuthorisationNumber, SequenceNumber);
                    }
                    else if (UKTraderId.HasValue && UKTrader.CFSPTypeId == CFSPType.Own)
                    {
                        ucr = CreateUCR(UKTrader.AuthorisationNumber, CFSPShipmentNumber);
                        isCFSPUCRUpdated = true;
                    }
                }

                await Database.Update(this, t =>
                {
                    t.Progress = status;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.UCR = ucr.Or(originalUCR);
                    t.CFSPUCRUpdated = isCFSPUCRUpdated;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(status);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsAccepted()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.ASMAccept;
                    t.NeedToSendAmendment = false;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.ASMAccept);
                //await SendNotificationEmail();

                await Shipment.UpdateProgress(Shipment);
                if (Shipment.IsInUK)
                {
                    await TriggerCommodityControlEmail();
                }
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsRejected(string error = null)
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.ASMReject;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.ASMReject);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();

                await Shipment.ErrorLog(error);
            });
        }

        public async Task FlagAsInternalError(string error = null)
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.InternalError;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.InternalError);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
                await Shipment.ErrorLog(error);

            });
        }

        public async Task FlagAsAwaitingArrivalDeparture()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                var status = this.Shipment.IsInUK ? Progress.AwaitingArrival : Progress.AwaitingDeparture;
                await Database.Update(this, t =>
                {
                    t.Progress = status;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(status);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsProcessingError(string error = null)
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                var status = this.Shipment.IsInUK ? Progress.ProcessingErrorArrival : Progress.ProcessingErrorDeparture;
                await Database.Update(this, t =>
                {
                    t.Progress = status;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(status);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();

                await Shipment.ErrorLog(error);
            });
        }

        public async Task FlagAsArrived(string route = "", string epu = "")
        {
            var entryValue = "";
            if (EntryReference.HasValue())
                entryValue = EntryReference.Substring(3);

            var entryReferenc = epu.HasValue() ? epu + entryValue : EntryReference;
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.Arrived;
                    t.Route = route;
                    t.EntryReference = entryReferenc;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.Arrived);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsWithCustoms(string route = "", string epu = "")
        {
            var entryReferenc = epu.HasValue() ? epu + EntryReference.Substring(3) : EntryReference;
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.WithCustoms;
                    t.Route = route;
                    t.EntryReference = entryReferenc;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.WithCustoms);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsQueriedArrived()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.QueriedArrived;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.QueriedArrived);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsQueriedWithCustoms()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.QueriedWithCustoms;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.QueriedWithCustoms);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }
        public async Task FlagAsLeftCountry()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.LeftCountry;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.LeftCountry);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsCleared()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.Cleared;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await Shipment.UpdateProgress(Shipment);

                await LogProgressHisoty(Progress.Cleared);

                if ((await this.Shipment.Consignments.GetList()).All(c => c.ProgressId == Progress.Cleared))
                    await Database.Update(Shipment, s => s.ConsignmentsClearedDate = LocalTime.Now, SaveBehaviour.BypassAll);

                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsCanceled()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.Cancelled;
                    t.LastStatusUpdate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(Progress.Cancelled);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
            });
        }

        public async Task FlagAsInvoiced()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t => t.IsInvoiced = true, SaveBehaviour.BypassAll);

                await ProcessNotificatoinEmail();

            });
        }

        public async Task FlagAsDraft()
        {
            var needToSendAmendment = false;

            if (Progress.IsAnyOf(Progress.AwaitingArrival,
                                 Progress.AwaitingDeparture,
                                 Progress.ProcessingErrorArrival,
                                 Progress.ProcessingErrorDeparture) || Progress.IsManual || (ProgressId == Progress.ASMAccept && Shipment.IsInUK))
                needToSendAmendment = true;

            var progressHistory = await ProgressHistory.GetList();
            if ((progressHistory.Any(x => x.Progress.IsAnyOf(Progress.AwaitingArrival,
                              Progress.AwaitingDeparture,
                              Progress.ProcessingErrorArrival,
                              Progress.ProcessingErrorDeparture)) || (progressHistory.Any(x => x.Progress.IsAnyOf(Progress.ASMAccept) && Shipment.IsInUK))))

                needToSendAmendment = true;

            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.Draft;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = LocalTime.Now;
                    t.NeedToSendAmendment = needToSendAmendment;
                }, SaveBehaviour.BypassAll);

                await Shipment.UpdateProgress(Shipment);

                await LogProgressHisoty(Progress.Draft);
            });
        }

        public async Task FlagAsDraft(int retries)
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.Draft;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = LocalTime.Now;
                    t.TransmitRetries = retries;
                }, SaveBehaviour.BypassAll);

                await Shipment.UpdateProgress(Shipment);

                await LogProgressHisoty(Progress.Draft);
            });
        }

        public async Task FlagAsDutyPayment()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.DutyPayment;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await Shipment.UpdateProgress(Shipment);
                await LogProgressHisoty(Progress.DutyPayment);
            });
        }

        public async Task FlagAsDutyPaymentWithAmendment()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.DutyPayment;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = LocalTime.Now;
                    t.NeedToSendAmendment = true;
                }, SaveBehaviour.BypassAll);

                await Shipment.UpdateProgress(Shipment);

                await LogProgressHisoty(Progress.DutyPayment);
            });
        }

        public async Task FlagAsHMRCControlled()
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = Progress.EntryControlled;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = LocalTime.Now;
                }, SaveBehaviour.BypassAll);

                await Shipment.UpdateProgress(Shipment);
                await ProcessNotificatoinEmail();
                await LogProgressHisoty(Progress.EntryControlled);
            });
        }

        public async Task UpdateEntryReference(string route = "", string epu = "")
        {
            var entryValue = "";
            if (EntryReference.HasValue())
                entryValue = EntryReference.Substring(3);

            var entryReferenc = epu.HasValue() ? epu + entryValue : EntryReference;
            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.EntryReference = entryReferenc;
                }, SaveBehaviour.BypassAll);
            });
        }

        public async Task Log(string str, LogType logType)
        {
            try
            {
                var filename = $"{logType}-{LocalTime.Now.ToLongTimeString()}{logType.GetFileExtension()}";

                await Database.Save(new EadTransactionLog
                {
                    Consignment = this,
                    Type = logType,
                    File = new Blob(System.Text.Encoding.UTF8.GetBytes(str), filename)
                });

            }
            catch (Exception ex)
            {
                Olive.Log.For<EadTransactionLog>().Error(ex);
            }
        }
        #endregion

        public async Task ManualUpdate(Progress status, User user = null)
        {
            DateTime? clearDate = null;
            if (status == Progress.Cleared)
                clearDate = LocalTime.Now;

            await Database.EnlistOrCreateTransaction(async () =>
            {
                await Database.Update(this, t =>
                {
                    t.Progress = status;
                    t.LastStatusUpdate = LocalTime.Now;
                    t.ClearedDate = clearDate;
                }, SaveBehaviour.BypassAll);

                await LogProgressHisoty(status, user);

                await Shipment.UpdateProgress(Shipment);

                if ((await this.Shipment.Consignments.GetList()).All(c => c.ProgressId == Progress.Cleared))
                    await Database.Update(Shipment, s => s.ConsignmentsClearedDate = LocalTime.Now);
                else
                    await Database.Update(Shipment, s => s.ConsignmentsClearedDate = null);
                await ProcessNotificatoinEmail();
            });
        }

        private async Task LogProgressHisoty(Progress progress, User user = null)
        {
            await Database.Save(new ConsignmentProgressHistory { Progress = progress, Consignment = this, User = user });
        }

        public async Task<IEnumerable<ConsignmentDocument>> GetDocuments(User user)
        {
            if ((user as ChannelPortsUser) != null)
                return await Documents.GetList().Where(t => t.File.FileExtension != ".png" && t.File.FileExtension != ".xml").OrderByDescending(x => x.DateRecieved);
            else
                return await Documents.GetList().Where(t => t.File.FileExtension == ".pdf" && t.File.FileExtension != ".xml").OrderByDescending(x => x.DateRecieved);
        }

        public async Task SetProgressToWithImporter(User user)
        {
            await Database.EnlistOrCreateTransaction(async () =>
            {
                //archive with importer
                await Database.Update(this, x =>
                {
                    x.Progress = Progress.WithImporter;
                    x.IsDeactivated = true;
                });
                var consigment = await Shipment.Consignments.Count();
                if (consigment == 1)
                {
                    await Database.Update(Shipment.Clone(), x =>
                    {
                        x.Progress = Progress.WithImporter;
                        x.IsDeactivated = true;
                    });
                }
                else
                {
                    await Shipment.UpdateProgress(Shipment);
                }
                await LogProgressHisoty(Progress.WithImporter, user);
                await EmailTemplate.SendArchiveNotification("Consigment", "Archive with Importer", user.Name, Shipment.TrackingNumber, Shipment.MyReferenceForCPInvoice);
            });
        }
        public bool IsImporterVisible(Progress progress)
        {
            if (progress.IsAnyOf(Progress.WithCustoms, Progress.QueriedArrived, Progress.QueriedWithCustoms, Progress.Cleared, Progress.WithImporter))
                return false;
            return true;
        }
        public async Task<bool> IsAbleToBeWithImporter() => this.Shipment.Type == ShipmentType.IntoUk;


        private async Task ProcessNotificatoinEmail()
        {

            if (Progress.RecieveEmailNotificationChannelport)
                await EmailTemplate.SendStatusNotificationEmail(Shipment.TrackingNumber, Shipment.MyReferenceForCPInvoice, Progress.AdminDisplay);

            if (Progress.RecieveEmailNotificationCustomer && Shipment.PrimaryContact.Email.HasValue())
                await EmailTemplate.SendCustomerStatusNotificationEmail(Shipment.PrimaryContact.Email, Shipment.TrackingNumber, Shipment.MyReferenceForCPInvoice, Progress.AdminDisplay);

            //for customer user 
            var haveProgress = await Database.Of<CustomerProgress>().Where(x => x.ProgressId == Progress && x.UserId == Shipment.CompanyId).FirstOrDefault();
            if (haveProgress?.RecieveEmailNotificationUserCustomer == true)
            {
                var allCustomer = await Database.Of<CompanyUser>().Where(x => x.CompanyId == Shipment.CompanyId && x.Email != Shipment.PrimaryContact.Email).GetList().ToList();
                foreach (var customerUser in allCustomer)
                {

                    await EmailTemplate.SendCustomerStatusNotificationEmail(customerUser.Email, Shipment.TrackingNumber, Shipment.MyReferenceForCPInvoice, Progress.AdminDisplay);
                }
            }
        }

        public (string Number, string Code, Company Company, bool IsUKTrader) GetDefermentNumber()
        {
            if (UKTrader.PaymentTypeId.HasValue && UKTrader.PaymentType.Code.ToStringOrEmpty().ToUpper() != "D")
                return (UKTrader.DefermentNumber.ToStringOrEmpty(), UKTrader.PaymentType.Code, UKTrader, true);
            else if (DeclarantId == Constants.ChannelPortsID && Shipment.Company.PaymentTypeId.HasValue && Shipment.Company.PaymentType.Code.ToStringOrEmpty().ToUpper() != "D") // if declarent is channel ports customer should check
                return (Shipment.Company.DefermentNumber.ToStringOrEmpty(), Shipment.Company.PaymentType.Code, Shipment.Company, false);
            else if (Declarant.PaymentTypeId.HasValue && Declarant.PaymentType.Code.ToStringOrEmpty().ToUpper() != "D")
                return (Declarant.DefermentNumber.ToStringOrEmpty(), Declarant.PaymentType.Code, Declarant, false);

            return (string.Empty, string.Empty, null, false);
        }


        public async Task<string> DutyMessage()
        {
            var defermentInfo = GetDefermentNumber();
            var hasStart2 = defermentInfo.Number.StartsWith("2");

            var defaultMessage = "Please arrange to top up your deposit so this shipment can be processed or in the event of a problem contact CustomsPro.";

            if (defermentInfo.Company == await Company.ChannelPort)
            {
                var ukTraderDeposit = await Deposit.HasCompanyRemainingBalancePositive(UKTrader);
                var companyDeposit = await Deposit.HasCompanyRemainingBalancePositive(Shipment.Company);

                if (!ukTraderDeposit && !companyDeposit)
                    return defaultMessage;
            }

            if (hasStart2 && !await Deposit.HasCompanyRemainingBalancePositive(defermentInfo.Company))
            {
                return defaultMessage;
            }

            if (!defermentInfo.IsUKTrader && hasStart2)
                return "Do you wish to use your deposit to clear this consignment? It will mean the duties are charged to you and there is no further opportunity to change it. If you are not sure click No and we will hold the entry pending your instructions";
            else if (defermentInfo.IsUKTrader && hasStart2)
                return $"{defermentInfo.Company.Name} has a deposit lodged with ourselves for the payment of duties. It will mean the duties are charged to them and there is no further opportunity to change it. If you are not sure click No and we will hold the entry pending your instructions";
            else if (defermentInfo.Number.HasAny())
                return $"We have the deferment Number of {defermentInfo.Number} which belongs to {defermentInfo.Company.Name}, Do you wish to send this information to HMRC? It will mean the duties are charged and there is no further opportunity to change it. If you are not sure click No and we will hold the entry pending your instructions";
            else
                return defaultMessage;
        }
        public bool IsDutyFree(IEnumerable<Commodity> commodities)
        {
            if (commodities.Any(x => x.CountryOfDestination.PreferenceAvailable == true && x.Product?.CommodityCode.FullRateOfDuty == 0m && x.Product?.CommodityCode.OtherQuota == false && x.Product?.CommodityCode.SpecificRate.To<double>() == 0))
                return true;

            return false;
        }
        public async Task<bool> HasControl(Consignment consignment)
        {
            return await consignment.Commodities.Any(x => x.Product.CommodityCode.Control);
        }
        public bool SendSDFOnlyMessage()
        {
            return Shipment.IsInUK && (!IsHaveAllDeclaraionDetails()); // first declaration should be similified
        }

        public bool IsHaveAllDeclaraionDetails()
        {
            return Shipment.IsInUK && HasFullCustomDetails ? true
                : UKTraderId.HasValue && UKTrader.SFDOnly == true ? false
                : true;
        }

        public bool IsHaveCFSP()
        {
            return Shipment.IsInUK && (!HasFullCustomDetails && (UKTraderId.HasValue && UKTrader.SFDOnly == true));
        }

        public async Task<bool> IsInventoryPort(Port port)
        {
            if (port != null)
            {
                var ports = await port.PortsIntoUk.GetList();
                return ports.Count() == 1 && ports.Any(x => x.IntoUKTypeId == PortType.Inventory);
            }
            return false;
        }
        public async Task<bool> IsGVMSPort(Port port)
        {
            if (port != null)
            {
                var ports = await port.PortsIntoUk.GetList();
                return ports.Count() == 1 && ports.Any(x => x.IntoUKTypeId == PortType.GVMS);
            }
            return false;
        }

        public async Task<bool> IsGVMSAndInventoryPort(Port port)
        {
            if (port != null)
            {
                var ports = await port.PortsIntoUk.GetList();
                return ports.Any(x => x.IntoUKTypeId == PortType.GVMS) && ports.Any(x => x.IntoUKTypeId == PortType.Inventory);
            }
            return false;
        }

        public async Task<bool> CanChangeEntryType()
        {
            if (!await IsGVMSAndInventoryPort(Shipment.Route?.UKPort))
            {
                return false;
            }

            return EntryReference.HasValue() && ProgressId == Progress.AwaitingArrival;
        }

        public async Task<bool> IntoUKTypeVisibilty(Port port)
        {
            if (port != null)
            {
                var ports = await port.PortsIntoUk.GetList();
                return ports.Any(x => x.IntoUKTypeId == PortType.GVMS) && ports.Any(x => x.IntoUKTypeId == PortType.Inventory);
            }
            return false;
        }

        public async Task<string> EntryTypeText(Port port)
        {
            if (port != null)
            {
                var ports = await port.PortsIntoUk.GetList();
                if ((ports.Count() == 1 && ports.Any(x => x.IntoUKTypeId == PortType.GVMS)) || IntoUKTypeId == PortType.GVMS)
                {
                    return "GVMS";
                }
                else if ((ports.Count() == 1 && ports.Any(x => x.IntoUKTypeId == PortType.Inventory)) || IntoUKTypeId == PortType.Inventory)
                {
                    return "INV";
                }
            }
            return "-";
        }
        public async Task<bool> Archive()
        {
            return this.ProgressId.IsAnyOf(Progress.Arrived, Progress.Cleared, Progress.LeftCountry) ||
                              await this.Documents.GetList().Any(x => x.File.FileName.ToUpper().Contains("X2") ||
                                                                      x.File.FileName.ToUpper().Contains("E2"));

        }
        public async Task<PortType> ConsigmentsPort(Port port)
        {
            if (port != null)
            {
                var ports = await port.PortsIntoUk.GetList();
                if (ports.Count() == 1 && ports.Any(x => x.IntoUKTypeId == PortType.Inventory))
                {
                    return ports.FirstOrDefault().IntoUKType;
                }
                else if (ports.Count() == 1 && ports.Any(x => x.IntoUKTypeId == PortType.GVMS))
                {
                    return ports.FirstOrDefault().IntoUKType;
                }
            }
            return null;
        }
        public async Task<bool> IsDefermentSent()
        {
            var lastLog = await Logs.Where(x => x.Type == LogType.CreateASMDeclarationRequest).OrderByDescending(x => x.Date).FirstOrDefault();
            if (lastLog == null)
                return false;
            var fileContent = await lastLog.File.GetContentTextAsync();
            return fileContent.Contains("firstDefermentPrefix") || fileContent.Contains("FirstDefermentNumber");
        }

        public async Task<string> GetSentDeferment()
        {
            var lastLog = await Logs.Where(x => x.Type == LogType.CreateASMDeclarationRequest).OrderByDescending(x => x.Date).FirstOrDefault();
            if (lastLog == null)
                return "";
            var fileContent = await lastLog.File.GetContentTextAsync();
            var xmlContent = fileContent.ToObjectFromXml<AsmImportGbDeclaration>();
            return xmlContent?.FirstDefermentNumber;
        }

        public async Task<IEnumerable<ConsignmentDocument>> GetDocuments<TKey>(User user,
          Expression<Func<ConsignmentDocument, bool>> filterCriteria = null,
          Func<ConsignmentDocument, TKey> orderBySelector = null,
          bool orderByDescending = false)
        {
            IEnumerable<ConsignmentDocument> result;
            var query = Documents;

            if ((user as ChannelPortsUser) != null)
                query = query.Where(t => t.File.FileExtension != ".png" && t.File.FileExtension != ".xml");
            else
                query = query.Where(t => t.File.FileExtension == ".pdf" && t.File.FileExtension != ".xml");

            //applying custom criteria
            if (filterCriteria != null)
                query = query.Where(filterCriteria);

            //loading data into memory
            result = await query.GetList();

            //ordering
            if (orderBySelector != null)
            {
                if (orderByDescending)
                    result = result.OrderByDescending(orderBySelector);
                else
                    result = result.OrderBy(orderBySelector);
            }

            return result;
        }

    }
}
