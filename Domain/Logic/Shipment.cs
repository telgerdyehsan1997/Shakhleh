namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Threading;
    using System.ComponentModel;

    partial class Shipment : ILoggable
    {
        static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);

        public override async Task Validate()
        {
            await base.Validate();

            if (VehicleNumber.IsEmpty() && TrailerNumber.IsEmpty())
                throw new ValidationException("At least one of the vehicle number or trailer number must be populated.");

            if (!Validator.IsValidVehicleNumber(VehicleNumber))
                throw new ValidationException("Vehicle number may only contain alphanumeric characters without any spaces.");

            if (!Validator.IsValidVehicleNumber(TrailerNumber))
                throw new ValidationException("Trailer number may only contain alphanumeric characters without any spaces.");

            if (!Validator.IsValidCustomerReference(MyReferenceForCPInvoice))
                throw new ValidationException("Customer Reference may only contain alphanumeric characters.");

            await Task.CompletedTask;
        }

        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            if (IsNew)
            {
                Progress = await GetStatus();
                Date = LocalTime.Now;
                TrackingNumber = await SetTrackingNumber();
            }
        }

        private async Task<string> SetTrackingNumber()
        {
            var firstLetter = IsInUK ? "R" : "T";
            var trackingNumberSuffix = await GetTrackingNumberSuffix();
            var trackingNumber = firstLetter + Date.Month.ToString("00") + Date.Year.ToString().Substring(2, 2) + trackingNumberSuffix;

            if (await Database.Of<Shipment>().Any(t => t.TrackingNumber == trackingNumber))
                return await SetTrackingNumber();

            return trackingNumber;
        }

        public async Task<string> GetTrackingNumberSuffix()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                var setting = await Database.Of<Settings>().FirstOrDefault();
                var suffix = "000000";

                if (IsInUK)
                {
                    suffix = setting.IntoUKTrackingNumber.ToString("000000");
                    await Database.Update(setting.Clone(), x => x.IntoUKTrackingNumber += 1);
                }
                else
                {
                    suffix = setting.OutOfUKTrackingNumber.ToString("000000");
                    await Database.Update(setting.Clone(), x => x.OutOfUKTrackingNumber += 1);
                }
                return suffix;
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        public async Task AreWeightsMismatched()
        {
            var consignments = await Consignments.GetList();
            var consignmentIds = consignments.Select(x => x.ID);
            var commodities = await Database.Of<Commodity>().Where(x => x.ConsignmentId.IsAnyOf(consignmentIds)).GetList();
            var mismatched = false;

            if (commodities.Select(x => x.ID).Any())
            {
                var consignmentTotalNetWeight = consignments.Sum(x => x.TotalNetWeight);
                var consignmentTotalGrossWeight = consignments.Sum(x => x.TotalGrossWeight);
                var commoditiesTotalNetWeight = commodities.Sum(x => x.NetWeight);
                var commoditiesTotalGrossWeight = commodities.Sum(x => x.GrossWeight);

                if (commoditiesTotalGrossWeight != consignmentTotalGrossWeight)
                    mismatched = true;

                if (commoditiesTotalNetWeight != consignmentTotalNetWeight)
                    mismatched = true;
            }

            if (mismatched)
                await Database.Update(this, x => IsWeightsMismatch = mismatched);
        }

        public async Task<Progress> GetStatus()
        {
            var consignments = await Consignments.GetList();

            if (consignments.None()) return Progress.Draft;

            if (consignments.GroupBy(t => t.Progress).IsSingle()) return consignments.First().Progress;

            return Progress.Partial;
        }

        public bool IsAdminAmendWithSingle => Task.Factory.RunSync(() => CanAmendWithSingle(isAdmin: true));
        public bool IsCustomerAmendWithSingle => Task.Factory.RunSync(() => CanAmendWithSingle(isAdmin: false));

        public bool IsEditable => Task.Factory.RunSync(() => CanEdit(isAdmin: true));

        public bool IsEditableCustomer => Task.Factory.RunSync(() => CanEdit(isAdmin: false));

        public async Task<bool> HasImportantChnage(Shipment shipment)
        {
            var oldShipmetn = await Database.Get<Shipment>(shipment.ID);
            if (shipment.Route.UKPortId != oldShipmetn.Route.UKPortId)
                return true;
            if (shipment.RouteId != oldShipmetn.RouteId)
                return true;

            return false;
        }

        async Task<bool> CanEdit(bool isAdmin)
        {
            if (isAdmin)
                return this.Progress.IsAnyOf(Helper.AdminEditableShipment) || (IsInUK && this.Progress.ID == Progress.ASMAccept);
            else
                return this.Progress.IsAnyOf(Helper.PublicEditableShipment);
        }

        async Task<bool> CanAmendWithSingle(bool isAdmin)
        {
            var consigmentCount = await this.Consignments.Count() == 1;
            if (isAdmin)
                return this.Progress.IsAnyOf(Helper.AdminEditShipment) && consigmentCount;
            else
                return this.Progress.IsAnyOf(Progress.DutyPayment) && consigmentCount;
        }

        public bool FullEdit => this.Progress.IsAnyOf(Progress.Draft, Progress.ASMReject);

        public bool IsOutUK => TypeId == ShipmentType.OutOfUk;

        public bool IsInUK => TypeId == ShipmentType.IntoUk;

        public override string ToString(string format)
        {
            var ukTraders = Consignments.GetList().Select(x => x.UKTrader.Name).ToString(" ").GetAwaiter().GetResult();
            return $"{TrackingNumber} {Date} {Type} {ExpectedDate} {Route.UKPort} {Route} {Company.Name} {VehicleNumber} " +
                $"{TrailerNumber} {MyReferenceForCPInvoice} {ukTraders}";
        }

        public async Task Log(string str, LogType logType)
        {
            try
            {
                await Database.Save(new ShipmentApiTransactionLog
                {
                    // Shipment = this,
                    Type = logType,
                    File = new Blob(System.Text.Encoding.UTF8.GetBytes(str), LocalTime.Now.ToLongTimeString())
                });

            }
            catch (Exception ex)
            {
                Olive.Log.For<EadTransactionLog>().Error(ex);
            }
        }

        public static async Task<Shipment> UpdateProgress(Shipment shipment)
        {
            var status = await shipment.GetStatus();
            if (status.IsAnyOf(Helper.LogShipmentStatus))
                await shipment.ErrorLog(status.AdminDisplay);

            if (status.IsAnyOf(Progress.DutyPayment, Progress.QueriedWithCustoms))
                await shipment.AccountingLog(status.AdminDisplay);

            var accountNotification = await Database.Of<AccountingNotification>().Where(l => l.ShipmentId == shipment && l.StatusId != SupportTicketStatus.Closed).GetList();
            if (accountNotification.Any())
                if (status.IsNoneOf(Progress.DutyPayment, Progress.QueriedWithCustoms))
                    await Database.Update(accountNotification, x =>
                    {
                        x.Action = SupportTicketAction.Close;
                        x.Status = SupportTicketStatus.Closed;
                    });

            var error = await Database.Of<ErrorLog>().Where(l => l.ShipmentId == shipment && l.StatusId != SupportTicketStatus.Closed).GetList();
            if (error.Any())
                if (status.IsNoneOf(Helper.LogShipmentStatus))
                    await Database.Update(error, x =>
                    {
                        x.Action = SupportTicketAction.Close;
                        x.Status = SupportTicketStatus.Closed;
                    });

            return (await Database.Update(shipment.Clone(), x => x.Progress = status, SaveBehaviour.BypassAll)).Clone();
        }

        public static async Task<IEnumerable<Shipment>> GetGVMSShipments(ShipmentType type, Company company, DateTime? expectedDateFrom, DateTime? expectedDateTo, string consignmentSearcheId)
        {
            var query = Database.Of<Shipment>().Where(x => x.IsGVMS == true);

            if (consignmentSearcheId.HasValue())
            {
                var parameters = await Database.Of<ConsignmentSearch>().Where(x => x.ID == new Guid(consignmentSearcheId)).FirstOrDefault();
                if (parameters != null)
                {
                    var shipmentIds = await ShipmentList(parameters);
                    query = query.Where(x => x.ID.IsAnyOf(shipmentIds));
                }

            }
            if (type != null)
                query = query.Where(s => s.TypeId == type);

            if (expectedDateFrom.HasValue)
                query = query.Where(x => x.ExpectedDate >= expectedDateFrom);

            if (expectedDateTo.HasValue)
                query = query.Where(x => x.ExpectedDate <= expectedDateTo.EndOfDay());

            if (company != null)
                query = query.Where(x => x.CompanyId == company);

            return await query.GetList();
        }


        public async Task<IEnumerable<string>> GetExtraInformer()
        {
            var result = new List<string>();

            if (GroupId.HasValue)
                result.AddRange(await Group.Contacts.Select(t => t.Email).ToList());

            result.AddRange(await ContactName.Select(t => t.Email).ToList());

            return result.ExceptNull();
        }

        public async Task<IEnumerable<Progress>> GetProgress()
        {
            if (ConsignmentsClearedDate.HasValue)
            {
                return await Database.Of<Progress>().Where(x => x.ClientDisplay != "Cancelled").GetList().ToList();
            }
            return await Database.Of<Progress>().GetList().ToList();
        }

        public static async Task<List<Guid?>> ShipmentList(ConsignmentSearch parameters)
        {

            var result = Database.Of<Consignment>();

            if (parameters.ConsignmentNumber.HasValue())
                result = result.Where(x => x.ConsignmentNumber.Contains(parameters.ConsignmentNumber.Trim()));
            if (parameters.UKTraderId.HasValue)
                result = result.Where(x => x.UKTraderId == parameters.UKTraderId);
            if (parameters.DeclarantId.HasValue)
                result = result.Where(x => x.DeclarantId == parameters.DeclarantId);
            if (parameters.TotalGrossWeightMin > 0)
                result = result.Where(x => x.TotalGrossWeight >= parameters.TotalGrossWeightMin);
            if (parameters.TotalGrossWeightMax > 0)
                result = result.Where(x => x.TotalGrossWeight <= parameters.TotalGrossWeightMax);
            if (parameters.InvoiceNumber.HasValue())
                result = result.Where(x => x.InvoiceNumber.Contains(parameters.InvoiceNumber));
            if (parameters.TotalValueMin > 0)
                result = result.Where(x => x.TotalValue >= parameters.TotalValueMin);
            if (parameters.TotalValueMax > 0)
                result = result.Where(x => x.TotalValue <= parameters.TotalValueMax);
            if (parameters.UCR.HasValue())
                result = result.Where(x => x.UCR.Contains(parameters.UCR));
            if (parameters.PartnerId.HasValue)
                result = result.Where(x => x.PartnerId == parameters.PartnerId);
            if (parameters.TotalPackagesMin > 0)
                result = result.Where(x => x.TotalPackages >= parameters.TotalPackagesMin);
            if (parameters.TotalPackagesMax > 0)
                result = result.Where(x => x.TotalPackages <= parameters.TotalPackagesMax);
            if (parameters.TotalNetWeightMin > 0)
                result = result.Where(x => x.TotalNetWeight >= parameters.TotalNetWeightMin);
            if (parameters.TotalNetWeightMax > 0)
                result = result.Where(x => x.TotalNetWeight <= parameters.TotalNetWeightMax);
            if (parameters.InvoiceCurrencyId.HasValue)
                result = result.Where(x => x.InvoiceCurrencyId == parameters.InvoiceCurrencyId);
            if (parameters.ProgressId.HasValue)
                result = result.Where(x => x.ProgressId == parameters.ProgressId);

            if (parameters.CommodityCode.HasValue())
            {
                var exportCode = parameters.CommodityCode.Split("-")[0].Trim();

                var productIds = await Database.Of<Product>().Where(x => x.Code == exportCode || x.CommodityCode.ExportCode == exportCode)
                    .GetList()
                    .Select(x => x.ID)
                    .ToList();

                var consignmentCommodities = await Database.Of<Commodity>()
                    .Where(x => x.ProductId.IsAnyOf(productIds))
                    .GetList()
                    .Select(x => x.ConsignmentId)
                    .ToList();

                result.Where(x => x.ID.IsAnyOf(consignmentCommodities));
            }

            return await result.GetList().Select(x => x.ShipmentId).ToList();
        }

        public string GetRouteName()
        {
            if (IsInUK)
                return Route?.Non_UKPort?.PortName + " to " + Route?.UKPort?.PortName;

            return Route?.UKPort?.PortName + " to " + Route?.Non_UKPort?.PortName;
        }

        public static async Task<IEnumerable<Shipment>> GetByTrackingNumber(IEnumerable<Shipment> result, string trackingNumber)
        {
            const int maxLength = 11;
            if (trackingNumber.HasValue())
            {
                trackingNumber = trackingNumber.Trim();
                if (trackingNumber.Length > maxLength)
                {
                    if (trackingNumber.EndsWith("01"))
                        trackingNumber = trackingNumber.TrimEnd("01");
                    if (trackingNumber.EndsWith("02"))
                        trackingNumber = trackingNumber.TrimEnd("02");
                }
                result = result.Where(n => n.TrackingNumber == trackingNumber);
            }
            return result;
        }

        public async Task ErrorLog(string str)
        {
            try
            {
                await Database.Save(new ErrorLog
                {
                    Shipment = this,
                    Error = str,
                    RecievedDate = LocalTime.Now
                });

            }
            catch (Exception ex)
            {
                Olive.Log.For<ErrorLog>().Error(ex);
            }
        }

        public async Task AccountingLog(string str)
        {
            try
            {
                await Database.Save(new AccountingNotification
                {
                    Shipment = this,
                    Error = str,
                    NotificationDate = LocalTime.Now
                });

            }
            catch (Exception ex)
            {
                Olive.Log.For<ErrorLog>().Error(ex);
            }
        }
        public async Task<bool> Archive()
        {
            var consigmentDocuments = await this.Consignments.GetList().Select(x => x.Documents.GetList());
            foreach (var document in consigmentDocuments.ToList())
            {
                if (document.Any(x => x.File.FileName.ToUpper().Contains("X2") || x.File.FileName.ToUpper().Contains("E2")))
                    return true;
            }
            return this.ProgressId.IsAnyOf(Progress.Arrived, Progress.Cleared, Progress.LeftCountry);
        }
    }
}