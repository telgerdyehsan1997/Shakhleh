using APIHandler;
using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IASMMapper
    {
        Task<AsmImportGbDeclaration> Map(Consignment consignment);
        Task<AsmExportGbDeclaration> MapExportEAD(Consignment consignment);
        ASMCustomReportRequest MapReportDeclarationRequest(Consignment consignment, string code = null);
        ASMFrontierCustomReportRequest MapFrontierReportDeclarationRequest(Consignment consignment, string code = null);
    }


    public class ASMMapper : IASMMapper
    {
        IDatabase Database;

        public ASMMapper(IDatabase database)
        {
            Database = database;
        }

        public async Task<AsmImportGbDeclaration> Map(Consignment consignment)
        {
            var port = consignment.Shipment.Route?.UKPort;
            try
            {
                var result = new AsmImportGbDeclaration
                {
                    DeclarationCurrency = new DeclarationCurrency { CurrencyCode = "GBP" },
                    DeclarationType = consignment.SequenceNumber.HasValue() ? DeclarationType.ISD : DeclarationType.IFD,
                    ConsignorParty = GetConsignor(consignment),
                    ConsigneeParty = GetConsignee(consignment),
                    DeclarantParty = GetDeclarant(consignment),
                    TotalPackages = consignment.TotalPackages,
                    Representation = "2",
                    DeclarationUcr = consignment.UCR,
                    DispatchCountry = new ASMCountry { Code = consignment.Partner.Country.Code },
                    TransportCountry = new ASMCountry { Code = "GB" },
                    BorderTransportMode = new EADBorderTransportMode { ModeOfTransportCode = port?.TransportMode ?? 0 },
                    GoodsLocationCountry = new ASMCountry { Code = "GB" },
                    GoodsLocationPort = new Location { IATAPortCode = port.TitledUsePortCode.HasValue() ? port.TitledUsePortCode : port.PortCode },
                    ProducedDocuments = GetProduceDocuments(consignment),
                    ReasonForAmendment = consignment.NeedToSendAmendment ? "Values updated" : "",
                    TransportIdentity = new[] { consignment.Shipment.VehicleNumber, consignment.Shipment.TrailerNumber }.ExceptNull().ToString("/"),
                };

                if (port.TitledLocationCode.HasValue() && consignment.Shipment.IsInUK)
                    result.GoodsLocationShed = port.TitledLocationCode;

                result.DeclarantBadge = new DeclarantBadge
                {
                    Code = Settings.Current.ActivateUCN == true && consignment.IntoUKTypeId == PortType.Inventory && port?.DTIBadge.HasValue() == true ? port.DTIBadge : AppSetting.BadgeCode,
                };

                if (consignment.IntoUKTypeId == PortType.Inventory && consignment.Shipment.IsInUK && Domain.Settings.Current.ActivateUCN == true)
                    result.MasterUcr = consignment.UCN.HasValue() ? consignment.UCN : null;

                if (consignment.Shipment.Company.BranchIdentifier.HasValue() || consignment.UKTrader.BranchIdentifier.HasValue() || consignment.Declarant.BranchIdentifier.HasValue() || consignment.Partner.BranchIdentifier.HasValue())
                {
                    var branchIdentifier = consignment.Shipment.Company.BranchIdentifier.Or(consignment.UKTrader.BranchIdentifier);
                    branchIdentifier = branchIdentifier.Or(consignment.Declarant.BranchIdentifier);
                    branchIdentifier = branchIdentifier.Or(consignment.Partner.BranchIdentifier);

                    result.AiStatements = result.AiStatements ?? new List<AIStatement>();
                    result.AiStatements.Add(new AIStatement { Code = branchIdentifier });
                }

                var ukTrader = consignment.UKTrader;

                var cfspType = (ukTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && ukTrader.UsingEIDR == true) || (ukTrader.CFSPTypeId.IsAnyOf(CFSPType.Own) && ukTrader.SFDOnly == false);
                if (consignment.IntoUKTypeId == PortType.Inventory && cfspType)
                    result.DeclarationType = DeclarationType.SFD;

                if (consignment.IntoUKTypeId == PortType.GVMS && cfspType)
                    result.DeclarationType = DeclarationType.ISD;

                if (consignment.Shipment.ExpectedDate > LocalTime.Now.AddHours(-24)
                    && consignment.IntoUKTypeId == PortType.GVMS
                    && ukTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && ukTrader.UsingEIDR == true)
                    result.DeclarationType = DeclarationType.ISD;

                if (consignment.ProgressId == Progress.Cleared && consignment.IntoUKTypeId == PortType.Inventory && cfspType)
                    result.DeclarationType = DeclarationType.ISD;


                if (ukTrader.CFSPTypeId.IsAnyOf(CFSPType.Own))
                    result.TraderReference = consignment.CFSPShipmentNumber;

                else if (ukTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && ukTrader.UsingEIDR == true)
                    result.TraderReference = consignment.CFSPShipmentNumber;
                else
                    result.TraderReference = $"{consignment.Shipment.MyReferenceForCPInvoice.DeepRemoveSpecials()}{consignment.IdNumber.ToString().PadLeft(2, '0')}{consignment.Shipment.Company.RefrenceSuffix}";


                // We need to add this into the message to ASM where there is a licence.
                var commoidites = await consignment.Commodities
                    .Where(x => x.GoodsLicencable && x.LicenceType.LicenceTypeId == LicenceType.Electronic.ID
                    && x.LicenceType.RPTID == true)
                    .GetList();

                if (commoidites.Any())
                {
                    result.AiStatements = result.AiStatements ?? new List<AIStatement>();
                    result.AiStatements.Add(new AIStatement { Code = "RPTID", Text = commoidites.FirstOrDefault().RPTIDCode });
                }

                if (consignment.SendSDFOnlyMessage())
                {
                    if (ukTrader.CFSPTypeId == CFSPType.Own && ukTrader.EORINumber != ukTrader.AuthorisationNumber)
                    {
                        result.AiStatements = new List<AIStatement>();
                        result.AiStatements.Add(new AIStatement { Code = "GEN46", Text = ukTrader.AuthorisationNumber });
                    }

                    if (ukTrader.AuthorisedCFSPCPCNumberId == CFSPCPCNumber._0612071)
                    {
                        result.WarehousePremise = new WarehousePremise { IdentityNumber = ukTrader.AuthorisedWarehouseNumber };
                        result.SupervisingOffice = new SupervisingOffice
                        {
                            City = ukTrader.LocalOfficerCity,
                            CountryCode = new ASMCountry { Code = ukTrader.LocalOfficerCountryCode?.Code },
                            Name = ukTrader.LocalOfficerSupervisingOffice,
                            PostCode = ukTrader.LocalOfficerPostcode.Replace(" ", "").GetSubString(9).ToUpper(),
                            Street = ukTrader.LocalOfficerStreet
                        };
                    }
                }
                else
                {
                    result.InvoiceCurrency = new APIHandler.Currency { CurrencyCode = consignment.InvoiceCurrency?.Name };
                    result.ValueBuildUp = GetValueBuildUp(consignment);
                }

                if (consignment.Shipment.ExpectedDate > new DateTime(2021, 12, 31) && consignment.IntoUKTypeId == PortType.GVMS)
                {
                    result.AiStatements = result.AiStatements ?? new List<AIStatement>();
                    result.AiStatements.Add(new AIStatement { Code = "RRS01" });
                }

                var commodities = await consignment.Commodities
                    .OrderBy(t => t.SubmitDate)
                    .GetList()
                    .ToList();

                if (consignment.HasPrefrenceForSubdivision.HasValue)
                {
                    var isGvms = consignment.IntoUKTypeId == PortType.GVMS;
                    result.SubDivision = consignment.HasPrefrenceForSubdivision == true && !(consignment.Shipment.ExpectedDate > new DateTime(2021, 12, 31)
                        && isGvms)
                        ? "IMA"
                        : "IMD";
                }
                else
                {
                    var subdivision = "";

                    var commodity = await consignment.Commodities.FirstOrDefault();
                    if (commodity.HasPreference == true)
                        subdivision = commodity.CountryOfDestination.ImportCPCWithPreferenceDeclarationType;
                    else
                        subdivision = commodity.CountryOfDestination.ImportCPCWithoutPreferenceDeclarationType;
                    result.SubDivision = subdivision + port?.IntoUKValue;
                }

                if (consignment.SendSDFOnlyMessage())
                {
                    result.SubDivision = "IMF";
                }
                var index = 1;
                var sum = 0m;
                var hasALine = false;
                const int totalValue = 6500;
                var hasValuation = !consignment.SendSDFOnlyMessage() && commodities.ToList()
                    .Any(t => (t.Product.CommodityCode.FullRateOfDuty > 0m || (t.Product.CommodityCode.FullRateOfDuty == 0m && t.Product.CommodityCode.OtherQuota == true))
                    && t.HasPreference != true)
                    && consignment.TotalValue > totalValue;

                foreach (var item in commodities)
                {
                    if (item.Consignment.SendSDFOnlyMessage() && result.Items.Any())
                        continue;

                    var commodityItem = await GetEADItem(item, index, hasValuation);
                    sum += commodityItem.Price ?? 0;
                    hasALine = hasALine ? hasALine : commodityItem.TaxLines.Any(t => t.Type.StartsWith("A"));
                    result.Items.Add(commodityItem);
                    index += 1;
                }

                if (!consignment.SendSDFOnlyMessage())
                    result.InvoiceAmount = sum;


                var defermentInfo = consignment.GetDefermentNumber();
                var methodOfPayments = result.Items.SelectMany(x => x.TaxLines).Except(x => x.MethodOfPayment == "G");
                var hasVATByDAN = defermentInfo.Company != null && defermentInfo.Company.VATByDAN == true;

                if (hasVATByDAN || (hasALine && methodOfPayments.Any(t => t.MethodOfPayment.HasValue())))
                {
                    if (defermentInfo.Number.StartsWith("2"))
                    {
                        var channelPorts = await Company.ChannelPort;
                        result.FirstDefermentPrefix = channelPorts.PaymentType.Code;
                        result.FirstDefermentNumber = channelPorts.DefermentNumber;
                        result.DeclarantParty = GetAsmAddress(channelPorts); //reassign it for channel port if it required the channel port defernment number
                    }
                    else if (defermentInfo.Number.HasValue())
                    {
                        result.FirstDefermentPrefix = defermentInfo.Code;
                        result.FirstDefermentNumber = defermentInfo.Number;
                    }
                }
                if (consignment.SequenceNumber.HasValue())
                {
                    result.SubDivision = "IMY";
                    result.TraderReference = consignment.SequenceNumber;
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.For<Consignment>().Error(ex);
                throw new ValidationException(ex.Message);
            }

        }

        public async Task<AsmExportGbDeclaration> MapExportEAD(Consignment consignment)
        {
            var port = consignment.Shipment.Route?.UKPort;
            var result = new AsmExportGbDeclaration
            {
                DeclarantBadge = new DeclarantBadge
                {
                    Code = Settings.Current.ActivateUCN == true && consignment.IntoUKTypeId == PortType.Inventory && port?.DTIBadge.HasValue() == true ? port?.DTIBadge : AppSetting.BadgeCode
                },
                DeclarationCurrency = new DeclarationCurrency { CurrencyCode = "GBP" },
                DeclarationType = DeclarationType.EFD,

                ConsignorParty = GetConsignor(consignment),
                ConsigneeParty = GetConsignee(consignment),
                DeclarantParty = GetDeclarant(consignment),
                TotalPackages = consignment.TotalPackages,
                // TraderReference = $"{consignment.Shipment.MyReferenceForCPInvoice}{consignment.IdNumber.ToString().PadLeft(2, '0')}{consignment.Shipment.Company.RefrenceSuffix}",
                Representation = 2,
                DeclarationUcr = consignment.UCR,
                DispatchCountry = new ASMCountry { Code = "GB" },
                TransportCountry = new ASMCountry { Code = consignment.Partner.Country.Code },
                TransportIdentity = (new[] { consignment.Shipment.VehicleNumber, consignment.Shipment.TrailerNumber }).ExceptNull().ToString("/"),
                InvoiceCurrency = new APIHandler.Currency { CurrencyCode = "GBP" },
                BorderTransportMode = new EADBorderTransportMode { ModeOfTransportCode = port?.TransportMode ?? 3 },
                GoodsLocationCountry = new ASMCountry { Code = "GB" },
                GoodsLocationPort = new Location { IATAPortCode = port.TitledUsePortCode.HasValue() ? port.TitledUsePortCode : port.PortCode },
                DestinationCountry = new ASMCountry { Code = (await consignment.Commodities.FirstOrDefault())?.CountryOfDestination?.Code },
                ReasonForAmendment = consignment.NeedToSendAmendment ? "Values updated" : "",

            };
            if (consignment.Shipment.Company.BranchIdentifier.HasValue() || consignment.UKTrader.BranchIdentifier.HasValue() || consignment.Declarant.BranchIdentifier.HasValue() || consignment.Partner.BranchIdentifier.HasValue())
            {
                var branchIdentifier = consignment.Shipment.Company.BranchIdentifier.Or(consignment.UKTrader.BranchIdentifier);
                branchIdentifier = branchIdentifier.Or(consignment.Declarant.BranchIdentifier);
                branchIdentifier = branchIdentifier.Or(consignment.Partner.BranchIdentifier);

                result.AiStatements = result.AiStatements ?? new List<AIStatement>();
                result.AiStatements.Add(new AIStatement { Code = branchIdentifier });
            }

            var ukTrader = consignment.UKTrader;

            if (ukTrader.CFSPTypeId.IsAnyOf(CFSPType.Own, CFSPType.Channelports))
            {
                if (Settings.Current.ChannelportsCFSPShipmentNumber.HasValue())
                    result.TraderReference = $"{Settings.Current.ChannelportsCFSPShipmentNumber}{consignment.IdNumber.ToString().PadLeft(2, '0')}{consignment.Shipment.Company.RefrenceSuffix}";
                else
                    result.TraderReference = $"{consignment.CFSPShipmentNumber}{consignment.IdNumber.ToString().PadLeft(2, '0')}{consignment.Shipment.Company.RefrenceSuffix}";
            }
            else
                result.TraderReference = $"{consignment.Shipment.MyReferenceForCPInvoice}{consignment.IdNumber.ToString().PadLeft(2, '0')}{consignment.Shipment.Company.RefrenceSuffix}";

            if (consignment.Shipment.SafetyAndSecurity == true)
            {
                var iteneris = await GetRoutingCountries(consignment).ToList();
                result.RoutingCountries = iteneris.Any() ? iteneris : null;
                result.TransportChargesPaymentMethod = "Y";
            }

            // We need to add this into the message to ASM where there is a licence.
            var coomidites = await consignment.Commodities
                .Where(x => x.GoodsLicencable && x.LicenceType.LicenceTypeId == LicenceType.Electronic.ID
                && (bool)x.LicenceType.RPTID == true)
                .GetList();

            if (coomidites.Any())
                result.AiStatements.Add(new AIStatement { Code = "RPTID", Text = coomidites.FirstOrDefault().RPTIDCode });

            result.ProducedDocuments.AddRange(GetProduceDocuments(consignment));

            //result.Seals.Add(new EADSeal { Number = string.Empty });

            var subdivision = "";
            var commodity = await consignment.Commodities.FirstOrDefault();
            if (commodity.HasPreference == true)
                subdivision = commodity.CountryOfDestination.ExportCPCWithPreferenceDeclarationType;
            else
                subdivision = commodity.CountryOfDestination.ExportCPCWithoutPreferenceDeclarationType;

            result.SubDivision = subdivision + port?.OutOfUKValue;

            var index = 1;
            var commodities = await consignment.Commodities
                .OrderBy(t => t.SubmitDate)
                .GetList()
                .ToList();
            foreach (var item in commodities)
            {
                result.Items.Add(await GetEADExportItem(item, index));
                index += 1;
            }

            if (port.OutOfUKTypeId == PortType.GVMS)
            {
                result.AiStatements = result.AiStatements ?? new List<AIStatement>();
                result.AiStatements.Add(new AIStatement { Code = "RRS01" });
            }

            return result;
        }

        public async Task<IEnumerable<AIStatement>> MapAiStatementsCommodities(IEnumerable<Commodity> commodities)
        {
            var aiStatements = new List<AIStatement>();

            commodities.Do(x =>
            {
                aiStatements.Add(new AIStatement { Code = "RPTID", Text = x.RPTIDCode });
            });

            return aiStatements;
        }

        public ASMCustomReportRequest MapReportDeclarationRequest(Consignment consignment, string code)
        {
            var reportname = "";
            if (code.HasValue())
            {
                reportname = "DTI-" + code;
            }
            else
            {
                reportname = "DTI-" + (consignment.Shipment.IsInUK ? "E2" : "X2");
            }
            return new ASMCustomReportRequest
            {
                DeclarationIdentity = new DeclarationIdentity
                {
                    DeclarationUcr = consignment.UCR,
                },
                ReportName = reportname
            };
        }

        public ASMFrontierCustomReportRequest MapFrontierReportDeclarationRequest(Consignment consignment, string code)
        {
            return new ASMFrontierCustomReportRequest
            {
                DUCR = new DUCR
                {
                    DeclarationUcr = consignment.UCR,
                }
            };
        }

        private string GetEORI(Consignment consignment)
        {
            if (consignment?.SpecialCPC?.OverrideEORI == true)
                return consignment.SpecialCPC.EORI;

            var commodities = Task.Factory.RunSync(() => consignment.Commodities.GetList());
            var cpc = new CPC();
            if (consignment.Shipment.IsOutUK)
            {
                if (commodities.Any(x => x.HasPreference == true))
                {
                    cpc = commodities.FirstOrDefault(x => x.HasPreference == true)?.CountryOfDestination.ExportCPCWithPreference;
                }
                else
                {
                    cpc = commodities.FirstOrDefault(x => x.HasPreference == false)?.CountryOfDestination.ExportCPCWithoutPreference;
                }
            }
            else if (!consignment.Shipment.IsOutUK)
            {
                if (commodities.Any(x => x.HasPreference == true))
                {
                    cpc = commodities.FirstOrDefault(x => x.HasPreference == true)?.CountryOfDestination.ImportCPCWithPreference;
                }
                else
                {
                    cpc = commodities.FirstOrDefault(x => x.HasPreference == false)?.CountryOfDestination.ImportCPCWithoutPreference;
                }
            }
            if (cpc != null)
            {
                return cpc.OverrideEORI == true ? cpc.EORI : null;
            }

            return null;

        }

        private AsmAddress GetConsignor(Consignment consignment)
        {
            var eori = GetEORI(consignment);
            return consignment.Shipment.IsOutUK ? GetAsmAddress(consignment.UKTrader, eori) : GetAsmAddress(consignment.Partner, eori, isPartner: true);
        }

        private AsmAddress GetConsignee(Consignment consignment)
        {
            var eori = GetEORI(consignment);
            return consignment.Shipment.IsOutUK ? GetAsmAddress(consignment.Partner, eori, isPartner: true) : GetAsmAddress(consignment.UKTrader, eori);
        }

        private AsmAddress GetDeclarant(Consignment consignment)
        {
            return GetAsmAddress(consignment.Declarant);
        }

        private AsmAddress GetAsmAddress(Company company, string eori = null, bool isPartner = false)
        {
            var traderIdentityNumber = "";
            if (!isPartner)
                traderIdentityNumber = eori.IsEmpty() ? company.EORINumber : eori;
            else
                traderIdentityNumber = null;

            return new AsmAddress
            {
                TraderIdentityNumber = traderIdentityNumber.GetSubString(Constants.TradeLenght),
                Name = company.Name.GetSubString(Constants.NameAndAddressLenght),
                Street = company.AddressStreet.GetSubString(Constants.NameAndAddressLenght),
                City = company.Town.GetSubString(Constants.NameAndAddressLenght),
                PostCode = company.Postcode.Replace(" ", "").GetSubString(9),
                Country = new ASMCountry { Code = company.Country.Code.GetSubString(2).ToUpper() },
                ShortCode = ""
            };
        }

        private ValueBuildUp GetValueBuildUp(Consignment consignment)
        {
            var result = new ValueBuildUp();


            if (consignment.Shipment.IsInUK)
            {
                if (consignment.FreightCurrencyId.HasValue && consignment.FreightAmount > 0)
                {
                    result.FreightChargeCurrency = new APIHandler.Currency { CurrencyCode = consignment.FreightCurrency?.Name };
                    result.FreightChargeAmount = consignment.FreightAmount.Value.ToString();
                }

                if (consignment.InsuranceCurrencyId.HasValue && consignment.InsuranceAmount > 0)
                {
                    result.InsuranceCurrency = new APIHandler.Currency { CurrencyCode = consignment.InsuranceCurrency.Name };
                    result.InsuranceAmount = consignment.InsuranceAmount.Value.ToString();
                }
            }

            if (consignment.TermsOfSale?.ValueForVAT > 0)
            {
                result.VatAdjustmentAmount = consignment.TermsOfSale.ValueForVAT.ToString("n");
                if (consignment.TermsOfSale.ValueForVAT > 0)
                    result.VatAdjustmentCurrency = new APIHandler.Currency { CurrencyCode = "GBP" };

            }

            return result;
        }

        private List<ProducedDocument> GetProduceDocuments(Consignment consignment)
        {
            var result = new List<ProducedDocument>();
            if (consignment.UKTrader.AEONumber.HasValue())
                result.Add(new ProducedDocument
                {
                    Code = "Y023",
                    Status = "JP",
                    Reference = consignment.UKTrader.AEONumber
                });

            if (consignment.Declarant.AEONumber.HasValue())
                result.Add(new ProducedDocument
                {
                    Code = "Y024",
                    Status = "JP",
                    Reference = consignment.Declarant.AEONumber
                });
            return result;
        }

        private async Task<List<ProducedDocument>> GetProduceCommodityDocuments(Commodity commodity)
        {
            var result = new List<ProducedDocument>();

            if (commodity.GoodsLicencable && commodity.LicenceTypeId.HasValue && commodity.Quantity > 0)
            {
                var reference = $"{commodity.LicenceType.LicenceIdentifier}/{commodity.LicenceNumber}";
                result.Add(new ProducedDocument
                {
                    Code = commodity.LicenceType?.ChiefLicenceCode,
                    Status = commodity.LicenceStatusCode?.StatusCode,
                    Reference = reference,
                    Quantity = commodity.Quantity
                });
            }

            var documents = GetProduceCommodityCodeDocuments(commodity);
            if (documents.Any())
                result.AddRange(documents);

            return result;
        }

        private List<ProducedDocument> GetProduceCommodityCodeDocuments(Commodity commodity)
        {
            var result = new List<ProducedDocument>();

            if (commodity.Product?.CommodityCode?.N851_PHC == true && commodity.NeedPHYTODocumentNumber == true)
                result.Add(new ProducedDocument
                {
                    Code = "N851",
                    Status = "AG",
                    Reference = "GBPHC" + commodity.PHYTODocumentNumber,
                });

            if (commodity.Product?.CommodityCode?.N852_CED == true && commodity.NeedIPAFFDocumentNumber == true)
                result.Add(new ProducedDocument
                {
                    Code = "N852",
                    Status = "AG",
                    Reference = "GBCED" + commodity.IPAFFDocumentNumber,
                });

            if (commodity.Product?.CommodityCode?.N853_CVD == true && commodity.NeedIPAFFDocumentNumber == true)
                result.Add(new ProducedDocument
                {
                    Code = "N853",
                    Status = "AE",
                    Reference = "GBCVD" + commodity.IPAFFDocumentNumber,
                });

            return result;
        }


        private async Task<EadInItem> GetEADItem(Commodity item, int index, bool hasValueation = false)
        {
            var preferenceCode = item.HasPreference == true ?
                                item.CountryOfDestination.ImportCPCWithPreferencePreferenceCode
                               : item.CountryOfDestination.ImportCPCWithoutPreferencePreferenceCode;


            var eadItem = new EadInItem
            {
                ItemNumber = index,
                OriginCountry = new ASMCountry { Code = item.CountryOfDestination.Code },
                AiStatements = new List<AIStatement>(),
            };


            if (!item.Consignment.SendSDFOnlyMessage())
            {
                var cpc = GetCPC(item);
                eadItem.Cpc = cpc.Number;
                eadItem.GoodsDescription = item.Product.Name.DeepRemoveSpecials();
                eadItem.CommodityCode = item.Product.CommodityCode.ExportCode + item.Product.CommodityCode.ImportCode;
                eadItem.PreferenceCode = preferenceCode;
                eadItem.Price = await GetCommodiyValue(item);
                eadItem.NetMass = item.NetWeight ?? 0;
                eadItem.ValuationAdjustmentPercentage = 0;

                if ((!item.Consignment.FreightAmount.HasValue || item.Consignment.FreightAmount == 0) && item.Consignment.TermsOfSale.IsDDP == false)
                    eadItem.ValuationAdjustment = "B";
                else if (item.Consignment.FreightAmount.HasValue && item.Consignment.FreightAmount > 0 && item.Consignment.TermsOfSale.IsDDP == false)
                    eadItem.ValuationAdjustment = "A";
                else if (item.Consignment.TermsOfSale.IsDDP == true)
                    eadItem.ValuationAdjustment = item.Consignment.TermsOfSale.Box45 ? "A" : "B";

                eadItem.TaxLines = cpc.NoTaxLine == true ? null : await GetTaxLines(item, cpc);

                //var specificRate = 0d;
                //if (item.Product.CommodityCode.SpecificRate.HasAny())
                //{
                //    specificRate = item.Product.CommodityCode.SpecificRate.To<double>();
                //}
                //var checkPreference = item.HasPreference == true && item.CountryOfDestinationId.HasValue && item.CountryOfDestination?.EU27 == true;
                //var dutycheck = item.Product.CommodityCode.FullRateOfDuty > 0;

                //if (item.Product?.AdditionalCode.HasValue() == true)
                //    eadItem.PreferenceCode = "100";

                //else if (checkPreference && dutycheck && item.Product.CommodityCode.SpecificRate.HasAny())
                //{
                //    eadItem.PreferenceCode = "300";
                //}
                //else if (checkPreference && item.Product.CommodityCode.FullRateOfDuty == 0 && specificRate == 0d && item.Product.CommodityCode.EUQuota.HasValue())
                //{
                //    eadItem.PreferenceCode = "120";
                //    eadItem.EuQuota = item.Product.CommodityCode.EUQuota;
                //}
                //else if (checkPreference && item.Product.CommodityCode.FullRateOfDuty == 0 && specificRate > 0d)
                //{
                //    eadItem.PreferenceCode = item.CountryOfDestination.ImportCPCWithPreferencePreferenceCode;
                //}
                //else if (item.Product.CommodityCode.EUQuota.HasValue() && item.Product.CommodityCode.EUQuotaPref.IsEmpty() && item.CountryOfDestinationId.HasValue && item.CountryOfDestination?.EU27 == true)
                //{
                //    eadItem.PreferenceCode = "120";
                //    eadItem.EuQuota = item.Product.CommodityCode.EUQuota;
                //}
                //else if (item.Consignment.Shipment.TypeId == ShipmentType.IntoUk.ID &&
                //    item.CountryOfDestination.EU27.HasValue &&
                //    !item.CountryOfDestination.EU27.Value &&
                //    await Database.Any<RowQuota>(x=>x.Countries.Contains(item.CountryOfDestination.Code)))
                //{
                //    eadItem.EuQuota = item.Product.CommodityCode.EUQuota;
                //    eadItem.PreferenceCode = "120";
                //}
                //else
                //{
                //    if (eadItem.TaxLines.None(t => t.Type.StartsWith("A")))
                //        eadItem.PreferenceCode = "100";

                //    else if (item.Product.CommodityCode.FullRateOfDuty == 0 && item.Product.CommodityCode.SpecificRate.HasAny())
                //        eadItem.PreferenceCode = "300";
                //}

                var checkPreference = item.HasPreference == true && item.CountryOfDestinationId.HasValue && item.CountryOfDestination?.EU27 == true;
                if (checkPreference && item.Product.CommodityCode.FullRateOfDuty > 0 && item.Product.CommodityCode.SpecificRate.HasAny())
                {
                    eadItem.PreferenceCode = "300";
                }
                else if (checkPreference && item.Product.CommodityCode.FullRateOfDuty == 0 && item.Product.CommodityCode.SpecificRate.HasAny())
                {
                    eadItem.PreferenceCode = "100";
                }
                else if (item.Product.CommodityCode.EUQuota.HasValue() || item.Product.CommodityCode.OtherQuota == true)
                {
                    eadItem.PreferenceCode = "120";
                    if (item.Consignment.Shipment.IsInUK && (item.CountryOfDestinationId.HasValue && item.CountryOfDestination?.EU27 == true) && item.Product.CommodityCode.EUQuota.HasValue())
                    {
                        eadItem.EuQuota = item.Product.CommodityCode.EUQuota;
                    }
                }
                else
                {
                    if (eadItem.TaxLines.None(t => t.Type.StartsWith("A")))
                        eadItem.PreferenceCode = "100";

                    else if (item.Product.CommodityCode.FullRateOfDuty == 0 && item.Product.CommodityCode.SpecificRate.HasAny())
                        eadItem.PreferenceCode = "300";
                }



                if (item.HasPreference == true)
                    eadItem.ProducedDocuments.Add(new ProducedDocument
                    {
                        Code = item.PreferenceTypeId == PreferenceType.InvoiceDeclaration ?
                                                         item.CountryOfDestination.InvoiceDeclarationDocumentType :
                                                         item.CountryOfDestination.PreferenceCertificateNumberDocumentType,

                        Status = item.CountryOfDestination.PreferenceCertificateNumberDocumentTypeDocumentStatus,
                        Reference = item.PreferenceTypeId == PreferenceType.PreferenceCertificateNumber ?
                                    item.PreferenceCertificateNumber.OrEmpty() :
                                    item.Consignment.InvoiceNumber
                    });

                if (hasValueation)
                {
                    eadItem.ValuationMethod = "1";
                    eadItem.ProducedDocuments.Add(new ProducedDocument
                    {
                        Code = Settings.Current.IntoUKDocumentCode,
                        Status = Settings.Current.IntoUKDocumentStatus,
                        Reference = Settings.Current.IntoUKDocumentReference,
                    });
                }
            }
            else
            {
                eadItem.Cpc = item.Consignment.UKTrader.AuthorisedCFSPCPCNumber.Name;
                eadItem.GoodsDescription = item.DescriptionOfGoods.DeepRemoveSpecials() ?? item.Product?.Name?.DeepRemoveSpecials();
            }

            if (item.Consignment.Shipment.TypeId == ShipmentType.OutOfUk && item.Consignment.Shipment.SafetyAndSecurity == true)
                eadItem.UnDangerousGoods = item.UNCode;

            if (item.SecondQuantity.HasValue && item.Product.CommodityCode.SecondQuantity != null)
                eadItem.SupplementaryUnits = item.SecondQuantity.ToString();

            if (item.ThirdQuantity.HasValue && item.Product.CommodityCode.ThirdQuantity != null)
                eadItem.ThirdQuantity = item.ThirdQuantity.ToString();

            PreviousDocuments(eadItem, item.Consignment.InvoiceNumber);

            if (item.Consignment.SecondInvoiceNumber.HasValue())
                PreviousDocuments(eadItem, item.Consignment.SecondInvoiceNumber);

            if (item.Consignment.ThirdInvoiceNumber.HasValue())
                PreviousDocuments(eadItem, item.Consignment.ThirdInvoiceNumber);

            if (item.Consignment.FourthInvoiceNumber.HasValue())
                PreviousDocuments(eadItem, item.Consignment.FourthInvoiceNumber);

            if (item.NumberOfPackages == 0)
                item.NumberOfPackages = 1;

            eadItem.Packages.Add(new Package
            {
                Kind = "PK",
                NumberOfPackages = item.NumberOfPackages ?? 1,
                Marks = "AS ADDR",
            });

            if (!item.Consignment.SendSDFOnlyMessage())
            {
                if (item.Consignment.Shipment.ExpectedDate < new DateTime(2022, 01, 01)) // need to remove after new year
                    eadItem.AiStatements.Add(new AIStatement { Code = "GEN53", Text = item.Consignment.Shipment.ExpectedDate.ToString("dd/MM/yyy") });
            }

            else
                eadItem.AiStatements.Add(new AIStatement { Code = "NCGDS" });

            if (item.Product?.CommodityCode.LIC99 == true)
                eadItem.AiStatements.Add(new AIStatement { Code = "LIC99" });

            eadItem.ProducedDocuments = await GetProduceCommodityDocuments(item);

            if (item.Consignment.Shipment.IsInUK && await item.Consignment.Shipment.Company.Products.Any(x => x.AdditionalCode.HasValue()))
                eadItem.CommodityCodeSupplemental = item.Product.AdditionalCode;

            return eadItem;
        }

        private static void PreviousDocuments(EadInItem eadItem, string number)
        {
            eadItem.PreviousDocuments.Add(new PreviousDocument
            {
                DocumentClass = "Z",
                Type = "380",
                Reference = number
            });
        }
        private static void PreviousDocuments(EadOutItem eadItem, string number)
        {
            eadItem.PreviousDocuments.Add(new PreviousDocument
            {
                DocumentClass = "Z",
                Type = "380",
                Reference = number
            });
        }

        private async Task<EadOutItem> GetEADExportItem(Commodity item, int index)
        {
            var cpc = GetCPC(item);

            var result = new EadOutItem
            {
                ItemNumber = index,
                CommodityCode = item.Product?.CommodityCode.ExportCode,
                GoodsDescription = item.Product?.Name.DeepRemoveSpecials(),
                OriginCountry = new ASMCountry { Code = "GB" },

                NetMass = item.NetWeight ?? 0,
                GrossMass = item.GrossWeight ?? 0,
                StatisticalValue = await item.GetGBPInvoice(),
                PreviousDocuments = new List<PreviousDocument>(),
                Packages = new List<Package>(),
                // Containers = item.Consignment.Shipment.ContainerNumber,
                //ProducedDocuments = new List<ProducedDocument>(),
                Cpc = cpc.Number,
                AiStatements = new List<AIStatement>(),

            };
            if (item.Consignment.Shipment.SafetyAndSecurity == true && item.Consignment.Shipment.ContainerNumber.HasValue())
                result.Containers.Add(new Container { Number = item.Consignment.Shipment.ContainerNumber });


            if (item.Consignment.Shipment.TypeId == ShipmentType.OutOfUk && item.Consignment.Shipment.SafetyAndSecurity == true)
                result.UnDangerousGoods = item.UNCode;

            if (item.SecondQuantity.HasValue && item.Product?.CommodityCode.SecondQuantity != null)
                result.SupplementaryUnits = item.SecondQuantity.ToString();

            if (item.ThirdQuantity.HasValue && item.Product?.CommodityCode.ThirdQuantity != null)
                result.ThirdQuantity = item.ThirdQuantity.ToString();

            if (item.Product?.CommodityCode.EUQuota.HasValue() == true && item.Consignment.Shipment.IsInUK)
                result.EuQuota = item.Product.CommodityCode.EUQuota;


            result.ProducedDocuments = await GetProduceCommodityDocuments(item);


            PreviousDocuments(result, item.Consignment.InvoiceNumber);

            if (item.Consignment.SecondInvoiceNumber.HasValue())
                PreviousDocuments(result, item.Consignment.SecondInvoiceNumber);

            if (item.Consignment.ThirdInvoiceNumber.HasValue())
                PreviousDocuments(result, item.Consignment.ThirdInvoiceNumber);

            if (item.Consignment.FourthInvoiceNumber.HasValue())
                PreviousDocuments(result, item.Consignment.FourthInvoiceNumber);


            if (item.NumberOfPackages == 0)
                item.NumberOfPackages = 1;

            result.Packages.Add(new Package
            {
                Kind = "PK",
                NumberOfPackages = item.NumberOfPackages ?? 1,
                Marks = "AS ADDR"
            });

            if (item.Product?.CommodityCode.LIC99 == true)
                result.AiStatements.Add(new AIStatement { Code = "LIC99" });


            return result;
        }

        private CPC GetCPC(Commodity item)
        {
            if (item.Consignment.SpecialCPC != null)
                return item.Consignment.SpecialCPC;

            if (item.Consignment.Shipment.IsInUK)
            {
                return item.HasPreference == true && item.CountryOfDestination.PreferenceAvailable == true ?
                        item.CountryOfDestination.ImportCPCWithPreference : item.CountryOfDestination.ImportCPCWithoutPreference;
            }
            else
                return item.HasPreference == true && item.CountryOfDestination.PreferenceAvailable == true ?
                            item.CountryOfDestination.ExportCPCWithPreference : item.CountryOfDestination.ExportCPCWithoutPreference;
        }

        //private async Task<List<APIHandler.TaxLine>> GetTaxLines(Commodity commodity, CPC cpc)
        //{
        //    var result = new List<APIHandler.TaxLine>();

        //    var taxlines = await cpc.TaxLines.Any() ? await cpc.TaxLines.GetList() : await Database.GetList<DefaultTaxLine>().Select(t => (TaxLine)t);

        //    foreach (var item in taxlines.ToList())
        //    {
        //        var commodityCode = commodity.Product?.CommodityCode;

        //        var countryCheck = commodity.CountryOfDestinationId.HasValue && commodity.CountryOfDestination?.EU27 == true;

        //        if (IsISAAndAdditionalCode(item, commodity) ||
        //            IsEU27AndPreferenceAndZeroFullRateOfDuty(item, commodity, commodityCode, countryCheck) ||
        //            IsEU27AndNotPreferenceANDQuotaNoPref(item, commodity, commodityCode, countryCheck) ||
        //                await IsIntoUKAndNotEU27WithRoWQuotas(commodity))
        //            TaxlineAdd(result, item);

        //        else if (!item.Isa || (item.Isa && commodityCode.FullRateOfDuty > 0m))
        //        {
        //            var rate = item.Rate.HasValue() ? item.Rate : GetRate(commodity, item);
        //            result.Add(new APIHandler.TaxLine
        //            {
        //                Type = item.Type,
        //                Rate = rate?.Length > 1 ? "Z" : rate,
        //                MethodOfPayment = GetMoP(commodity, item),
        //                BaseAmount = item.BaseAmount.IsEmpty() ? null : item.BaseAmount,
        //                BaseQuantity = item.BaseQuantity.IsEmpty() ? null : item.BaseQuantity,
        //                Amount = item.Amount.IsEmpty() ? null : item.Amount,
        //                OverrideCode = item.Override.IsEmpty() ? null : item.Override,
        //            });
        //        }

        //        //else if (countryCheck && commodity.HasPreference == true && commodityCode.FullRateOfDuty == 0)
        //        //    TaxlineAdd(result, item);

        //        //else if (countryCheck && commodity.HasPreference == false && commodityCode.EUQuota.HasValue() && commodityCode.EUQuotaPref.IsEmpty())
        //        //    TaxlineAdd(result, item);

        //        //else if (commodity.Consignment.Shipment.TypeId == ShipmentType.IntoUk.ID &&
        //        //    commodity.CountryOfDestination.EU27.HasValue &&
        //        //    !commodity.CountryOfDestination.EU27.Value &&
        //        //    await commodity.CountryOfDestination.RowQuotas.Any())
        //        //    TaxlineAdd(result, item);
        //    }

        //    return result;
        //}


        private async Task<List<APIHandler.TaxLine>> GetTaxLines(Commodity commodity, CPC cpc)
        {
            var result = new List<APIHandler.TaxLine>();

            var taxlines = await cpc.TaxLines.Any() ? await cpc.TaxLines.GetList() : await Database.GetList<DefaultTaxLine>().Select(t => (TaxLine)t);

            foreach (var item in taxlines.ToList())
            {
                var commodityCode = commodity.Product?.CommodityCode;
                var hasFullRate = commodityCode.FullRateOfDuty > 0m || (commodityCode.FullRateOfDuty == 0m && commodityCode.HasSpecificRate);

                if (!item.Isa || (item.Isa && hasFullRate))
                {
                    var rate = item.Rate.HasValue() ? item.Rate : GetRate(commodity, item);
                    result.Add(new APIHandler.TaxLine
                    {
                        Type = item.Type,
                        Rate = rate?.Length > 1 ? "Z" : rate,
                        MethodOfPayment = GetMoP(commodity, item),
                        BaseAmount = item.BaseAmount.IsEmpty() ? null : item.BaseAmount,
                        BaseQuantity = item.BaseQuantity.IsEmpty() ? null : item.BaseQuantity,
                        Amount = item.Amount.IsEmpty() ? null : item.Amount,
                        OverrideCode = item.Override.IsEmpty() ? null : item.Override,
                    });
                }
            }

            return result;
        }


        private bool IsISAAndAdditionalCode(TaxLine item, Commodity commodity) => item.Isa && commodity.Product?.AdditionalCode.HasValue() == true;

        private bool IsEU27AndPreferenceAndZeroFullRateOfDuty(TaxLine item, Commodity commodity, CommodityCode commodityCode, bool countryCheck) => countryCheck && commodity.HasPreference == true && commodityCode.FullRateOfDuty == 0;

        private bool IsEU27AndNotPreferenceANDQuotaNoPref(TaxLine item, Commodity commodity, CommodityCode commodityCode, bool countryCheck) => countryCheck && commodity.HasPreference == false && commodityCode.EUQuota.HasValue() && commodityCode.EUQuotaPref.IsEmpty();

        private async Task<bool> IsIntoUKAndNotEU27WithRoWQuotas(Commodity commodity)
        {
            return commodity.Consignment.Shipment.TypeId == ShipmentType.IntoUk.ID &&
                    commodity.CountryOfDestination.EU27.HasValue &&
                    !commodity.CountryOfDestination.EU27.Value &&
                    await Database.Any<RowQuota>(x => x.Countries.Contains(commodity.CountryOfDestination.Code));
        }


        private static void TaxlineAdd(List<APIHandler.TaxLine> result, TaxLine item)
        {
            result.Add(new APIHandler.TaxLine
            {
                Type = "A00",
                Rate = "F",
                MethodOfPayment = null,
                BaseAmount = item.BaseAmount.IsEmpty() ? null : item.BaseAmount,
                BaseQuantity = item.BaseQuantity.IsEmpty() ? null : item.BaseQuantity,
                Amount = item.Amount.IsEmpty() ? null : item.Amount,
                OverrideCode = item.Override.IsEmpty() ? null : item.Override,
            });
        }

        private string GetRate(Commodity commodity, TaxLine item)
        {
            if (item.Isa)
            {
                if (commodity.HasPreference == true || (bool)commodity.Product?.CommodityCode?.HasSpecificRate)
                    return commodity.CountryOfDestination.ImportCPCWithPreferenceRateCode;
                else
                    return commodity.CountryOfDestination.ImportCPCWithoutPreferenceRateCode;
            }
            else
                return commodity.VATId.HasValue ? commodity.VAT?.Name : commodity.Product?.VAT?.Name;
        }

        private string GetMoP(Commodity commodity)
        {
            if (commodity.HasPreference == true || (bool)commodity.Product?.CommodityCode?.HasSpecificRate)
                return null;

            var company = commodity.Consignment.UKTrader;

            if (company.PaymentTypeId == null)
                company = commodity.Consignment.Declarant;

            if (commodity.Product?.CommodityCode?.EUQuota?.HasValue() == true)
                return "F";

            if (company.PaymentTypeId == null)
                return null;

            if (company.PaymentType.Code.ToLower().IsAnyOf("a", "b", "c"))
                return "F";

            return company.PaymentType.Code.ToUpper();
        }

        private string GetMoP(Commodity commodity, TaxLine tax)
        {
            if (tax.Isa)
                return tax.MoP.IsEmpty() ? GetMoP(commodity) : tax.MoP;
            else
            {
                var vatType = (commodity.VATId.HasValue ? commodity.VAT?.Name : commodity.Product?.VAT?.Name).ToUpper();
                if (vatType == "Z")
                    return null;
                else if (vatType == "S")
                {
                    var company = commodity.Consignment.UKTrader;

                    if (company.PaymentTypeId == null)
                        company = commodity.Consignment.Declarant;
                    if (company.VATByDAN == true)
                        return "F";

                    else
                        return tax.MoP.IsEmpty() ? "G" : tax.MoP;
                }
                else
                    return tax.MoP.IsEmpty() ? "G" : tax.MoP;
            }
        }

        private async Task<decimal> GetCommodiyValue(Commodity commodity)
        {
            var consignment = commodity.Consignment;
            var value = 0m;

            if (consignment.TermsOfSaleId == null || consignment.TermsOfSale?.IsDDP == false || consignment.Shipment.IsOutUK)
                value = commodity.Value ?? 0m;
            else
            {
                var wtoRate = commodity.HasPreference == true ? 0 : decimal.ToDouble(commodity.Product.CommodityCode.FullRateOfDuty);

                if (wtoRate <= 0 && commodity.Product.CommodityCode.OtherQuota == false)
                    return commodity.Value ?? 0m;

                if (consignment.DDPOptionsId == DDPType.DutyInclusive)
                    value = (commodity.Value ?? 0m) / (decimal)(1 + (wtoRate * 0.01));

                else if (consignment.DDPOptionsId == DDPType.DutyAndVATInclusive)
                {
                    var vat = commodity.VAT.Name.ToUpper();
                    var vatRate = (await Database.GetList<VATRate>()
                            .Where(t => !t.IsDeactivated).OrderByDescending(t => t.ValidFrom))
                            .FirstOrDefault();
                    var vatValue = 0;
                    switch (vat)
                    {
                        case "S":
                            vatValue = vatRate.RateS;
                            break;
                        case "A":
                            vatValue = vatRate.RateA;
                            break;
                        case "Z":
                            vatValue = vatRate.RateZ;
                            break;
                        default:
                            break;
                    }
                    value = (commodity.Value ?? 0m) / (decimal)(1 + (vatValue * 0.01));
                    value /= (decimal)(1 + (wtoRate * 0.01));
                }
            }

            return value;
        }

        private async Task<IEnumerable<ASMCountry>> GetRoutingCountries(Consignment consignment)
        {
            var iterneatries = await Route.GetItineries(consignment);

            return iterneatries.Select(x => new ASMCountry
            {
                Code = x.Code
            });
        }

        private List<ProducedDocument> GetCommodityN8Document(Commodity commodity)
        {
            var result = new List<ProducedDocument>();
            var reference = $"{commodity.LicenceType.LicenceIdentifier}/{commodity.LicenceNumber}";
            if (commodity.Product?.CommodityCode?.N851_PHC == true)
                result.Add(new ProducedDocument
                {
                    Code = "N851",
                    Status = "AG",
                    Reference = commodity.PHYTODocumentNumber,
                });

            if (commodity.Product?.CommodityCode?.N852_CED == true)
                result.Add(new ProducedDocument
                {
                    Code = "N852",
                    Status = "AG",
                    Reference = commodity.IPAFFDocumentNumber,
                });

            if (commodity.Product?.CommodityCode?.N853_CVD == true)
                result.Add(new ProducedDocument
                {
                    Code = "N853",
                    Status = "AG",
                    Reference = commodity.IPAFFDocumentNumber,
                });

            return result;
        }
    }
}
