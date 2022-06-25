using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIContracts;
using Olive;
using Olive.Entities;
using Olive.Entities.Data;

namespace Domain
{
    public class EADShipmentMapper
    {
        static IDatabase Database => Context.Current.Database();

        public async static Task MapShipmentContract(ShipmentContract shipmentContract, Company company)
        {
            try
            {

                var driverMobileCountry = new Country();
                var port = new Port();
                var route = new Route();

                if (shipmentContract.RouteUKPortCode.HasValue() && shipmentContract.RouteNonUKPortCode.HasValue())
                {
                    route = await Route.GetRoute(shipmentContract.RouteUKPortCode, shipmentContract.RouteNonUKPortCode);
                    if (route == null)
                        throw new ValidationException($"Unable to found the possible route from {shipmentContract.RouteUKPortCode}, {shipmentContract.RouteNonUKPortCode}.");
                }
                else
                {
                    port = await Port.GetActivePortByCode(shipmentContract.PortCode);
                    if (port == null)
                        throw new ValidationException("Invalid PortCode or port not found.");

                    route = await Route.GetRoute(port);
                }


                var authorisedLocation = new AuthorisedLocation();

                if (shipmentContract.UseAuthorisedLocation)
                    authorisedLocation = await GetCompanyAuthorisedLocation(shipmentContract.AuthorisedLocationCustomsIdentity, company);
                else
                    authorisedLocation = null;

                if (!Validator.IsValidCustomerReference(shipmentContract.CustomerReference))
                    throw new ValidationException("Customer Reference only accepts alphanumeric characters.");

                if (!company.PrimaryContactId.HasValue)
                    company.PrimaryContact = await Database.FirstOrDefault<Contact>(x => x.CompanyId == company);

                var shipment = new Shipment
                {
                    Company = company,
                    PrimaryContact = company.PrimaryContact,
                    MyReferenceForCPInvoice = shipmentContract.CustomerReference,
                    Type = shipmentContract.IsIntoUK ? ShipmentType.IntoUk : ShipmentType.OutOfUk,
                    VehicleNumber = shipmentContract.VehicleNumber,
                    TrailerNumber = shipmentContract.TrailerNumber,
                    ExpectedDate = shipmentContract.ExpectedDate,
                    NotifyAdditionalParty = NotifyType.NotRequired,
                    Route = route,
                    IsAPI = true,
                    IsDraft = shipmentContract.IsDraft,
                    UseAuthorisedLocation = shipmentContract.UseAuthorisedLocation,
                    AuthorisedLocation = authorisedLocation
                };
                if ((shipmentContract.IsIntoUK && company.SafetyAndSecurityInboundId == SafetyAndSecurity.Sometimes) || (shipmentContract.IsIntoUK == false && company.SafetyAndSecurityOutboundId == SafetyAndSecurity.Sometimes))
                {
                    shipment.SafetyAndSecurity = shipmentContract.SafetyAndSecurity;
                }
                else if ((shipmentContract.IsIntoUK && company.SafetyAndSecurityInboundId == SafetyAndSecurity.Always) || (shipmentContract.IsIntoUK == false && company.SafetyAndSecurityOutboundId == SafetyAndSecurity.Always))
                {
                    shipment.SafetyAndSecurity = true;
                }
                else
                {
                    shipment.SafetyAndSecurity = false;
                }
                if (shipmentContract.SafetyAndSecurity && shipmentContract.IsIntoUK)
                {
                    if (shipmentContract.Carrier == null)
                        throw new ValidationException("Carrier must have a value.");
                }
                var lstAttachments = new List<UploadAttachment>();

                foreach (var attachment in shipmentContract.AttachmentContracts)
                {
                    if (attachment.Name.HasValue() && attachment.Attachment.IsEmpty())
                        throw new ValidationException("Attachment Name and Attachment must have a value.");
                    if (attachment.Attachment.HasValue() && attachment.Name.IsEmpty())
                        throw new ValidationException("Attachment Name and Attachment must have a value.");
                    if (attachment.Name.HasValue() && attachment.Attachment.HasValue())
                    {
                        var attachmentFile = new UploadAttachment();
                        var bytes = Convert.FromBase64String(attachment.Attachment);
                        attachmentFile.Shipment = shipment;
                        attachmentFile.Attachment = new Blob(bytes, $"{attachment.Name}");
                        lstAttachments.Add(attachmentFile);
                    }
                }
                shipmentContract.ImportedAttachments = lstAttachments;

                shipmentContract.ImportedShipment = shipment;
                await MapCarrier(shipmentContract, company);
                await MapBorderCrossingAndOfficeDestination(shipmentContract);

                await shipmentContract.Consignments.Do(async t => await MapConsignmentContract(t, shipment, shipmentContract.IsIntoUK));
            }
            catch (Exception ex)
            {
                Log.For<Shipment>().Error(ex);
                throw;
            }
        }

        private static async Task<AuthorisedLocation> GetCompanyAuthorisedLocation(string authorisedLocation, Company company)
        {
            var objAuthorisedLocation = await Database.FirstOrDefault<AuthorisedLocation>(x => x.CustomsIdentity == authorisedLocation && !x.IsDeactivated);
            var companyAuthorisedLocationsLink = await Database.FirstOrDefault<CompanyAuthorisedLocationsLink>(x => x.CompanyId == company
                && x.AuthorisedlocationId == objAuthorisedLocation);
            return companyAuthorisedLocationsLink?.Authorisedlocation ?? throw new ValidationException("Invalid Company Authorised Location or does not exist.");
        }

        public async static Task MapConsignmentContract(ConsignmentContract consignmentContract, Shipment shipment, bool isIntoUK = false)
        {
            var invoiceCurrency = await Database.FirstOrDefault<Currency>(x => x.Name == consignmentContract.InvoiceCurrency && x.IsDeactivated == false);
            var freightCurrency = await Database.FirstOrDefault<Currency>(x => x.Name == consignmentContract.FreightCurrency && x.IsDeactivated == false);
            var insuranceCurrency = await Database.FirstOrDefault<Currency>(x => x.Name == consignmentContract.InsuranceCurrency && x.IsDeactivated == false);
            var isImporterPayingTheFreight = false;
            var isImporterPayingInsuranceCharges = false;
            var termsOfSale = await Database.FirstOrDefault<TermOfSale>(x => x.Name == consignmentContract.TermsOfSale);

            var ddpOption = await DDPType.FindByName(consignmentContract.DDPOption);

            if (termsOfSale == null)
                throw new ValidationException("TermsOfSale is invalid.");

            if (termsOfSale != null && termsOfSale.IsDDP == true && ddpOption == null)
                throw new ValidationException("DDPOption is invalid.");

            //if (shipment.IsOutUK && termsOfSale?.IsDDP == true) 
            //    throw new ValidationException("TermsOfSale cannot be DDP for shipment that is out of the UK.");

            if (invoiceCurrency == null) { throw new ValidationException("Invalid Invoice Currency or does not exist."); }

            if (freightCurrency != null) { isImporterPayingTheFreight = true; }
            else if (freightCurrency == null && consignmentContract.FreightCurrency.HasValue()) { throw new ValidationException("Invalid Freight Currency or does not exist."); }

            if (insuranceCurrency != null) { isImporterPayingInsuranceCharges = true; }
            else if (insuranceCurrency == null && consignmentContract.InsuranceCurrency.HasValue())
                throw new ValidationException("Invalid Insurance Currency or does not exist.");


            var useSpecialCPC = false;
            var specialCPC = new CPC();

            if (consignmentContract.SpecialCPC.HasValue())
            {
                specialCPC = await GetCompanyCPC(consignmentContract.SpecialCPC, shipment.Company);
                if (specialCPC != null) { useSpecialCPC = true; }
            }
            consignmentContract.ImportedConsignment = new Consignment
            {
                Shipment = shipment,
                UseSpecialCPC = useSpecialCPC,
                SpecialCPC = consignmentContract.SpecialCPC.HasValue() ? specialCPC : null,
                TotalPackages = consignmentContract.TotalPackages,
                TotalGrossWeight = consignmentContract.TotalGrossWeight,
                TotalNetWeight = consignmentContract.TotalNetWeight,
                InvoiceNumber = consignmentContract.InvoiceNumber,
                SecondInvoiceNumber = consignmentContract.SecondInvoiceNumber,
                ThirdInvoiceNumber = consignmentContract.ThirdInvoiceNumber,
                FourthInvoiceNumber = consignmentContract.FourthInvoiceNumber,
                InvoiceCurrency = invoiceCurrency,
                TotalValue = consignmentContract.TotalValue,
                IsImporterPayingTheFreight = isImporterPayingTheFreight,
                FreightCurrency = freightCurrency,
                FreightAmount = consignmentContract.FreightAmount,
                IsImporterPayingInsuranceCharges = isImporterPayingInsuranceCharges,
                InsuranceCurrency = insuranceCurrency,
                InsuranceAmount = consignmentContract.InsuranceAmount,
                LRN = shipment.IsNCTSShipmentOutConvertible == true ? consignmentContract.LRN : string.Empty,
                TermsOfSale = consignmentContract.TermsOfSale.HasValue() ? termsOfSale : null,
                DDPOptions = consignmentContract.DDPOption.HasValue() ? ddpOption : null,
                UCN = consignmentContract.UCN,
                IntoUKType = await shipment.Route.UKPort.PortsIntoUk.Count() > 1 ? PortType.GVMS : null
            };
            if (consignmentContract.UCN.HasValue() && await shipment.Route.UKPort.PortsIntoUk.Count() > 1)
                consignmentContract.ImportedConsignment.IntoUKType = PortType.Inventory;


            //always 1
            //consignmentContract.ImportedConsignment.IdNumber = await consignmentContract.ImportedConsignment.GenerateIdNumber(shipment);

            await MapCompanies(consignmentContract);

            var totalgross = consignmentContract.CommodityContracts.Sum(x => x.GrossWeight);
            if (!totalgross.AlmostEquals(consignmentContract.ImportedConsignment.TotalGrossWeight.Value) && !shipment.IsDraft)
                throw new ValidationException("Consignment total gross weight must be equal to commodities gross weight");

            var totalnet = consignmentContract.CommodityContracts.Sum(x => x.NetWeight);
            if (!totalnet.AlmostEquals(consignmentContract.ImportedConsignment.TotalNetWeight.Value) && !shipment.IsDraft)
                throw new ValidationException("Consignment total net weight must be equal to commodities net weight");

            await consignmentContract.CommodityContracts.Do(async t => await MapCommodityContract(t, consignmentContract.ImportedConsignment,
                shipment.Company, consignmentContract, isIntoUK, shipment.SafetyAndSecurity == true));

        }

        public async static Task MapCompanies(ConsignmentContract consignmentContract)
        {
            var uKTraderCountry = await Country.FindByCodeOrName(consignmentContract.UKTrader.CountryCode, consignmentContract.UKTrader.CountryName, "UKTrader");
            var partnerCountry = await Country.FindByCodeOrName(consignmentContract.Partner.CountryCode, consignmentContract.Partner.CountryName, "Partner");
            var declarantCountry = await Country.FindByCodeOrName(consignmentContract.Declarant.CountryCode, consignmentContract.Declarant.CountryName, "Declarant");

            if (uKTraderCountry.Code == "GB" && consignmentContract.UKTrader.PostCode.IsEmpty())
                throw new ValidationException("UKTrader Postcode must have a value.");
            if (uKTraderCountry.Code == "GB" && consignmentContract.UKTrader.EORI.IsEmpty())
                throw new ValidationException("UKTrader EORI must have a value.");

            if (consignmentContract.UKTrader.EORI.IsEmpty())
                throw new ValidationException("UKTrader  must have EORI number.");

            if (consignmentContract.UKTrader.EORI.StartsWith("GB") == false)
                throw new ValidationException("UKTrader EORI must start with GB.");

            if (partnerCountry == await Country.GetUK())
                throw new ValidationException("Partner country can not be United Kingdom (GB).");
            if (consignmentContract.Partner.PostCode.IsEmpty())
                throw new ValidationException("Partner Postcode must have a value.");


            if (consignmentContract.Declarant.EORI.IsEmpty())
                throw new ValidationException("Declarant must have EORI number.");

            if (consignmentContract.Declarant.EORI.StartsWith("GB") == false)
                throw new ValidationException("Declarant EORI must start with GB.");
            if (declarantCountry?.Code == "GB" && consignmentContract.Declarant.PostCode.IsEmpty())
                throw new ValidationException("Declarant Postcode must have a value.");
            if (declarantCountry.Code == "GB" && consignmentContract.Declarant.EORI.IsEmpty())
                throw new ValidationException("Declarant EORI must have a value.");

            var ukPaymentType = await GetPaymentType(consignmentContract.UKTrader.PaymentCode);
            var partnerPaymentType = await GetPaymentType(consignmentContract.Partner.PaymentCode);
            var declarantPaymentType = await GetPaymentType(consignmentContract.Declarant.PaymentCode);

            consignmentContract.ImportedConsignment.UKTrader = consignmentContract.ImportedUkTrader = await new Company
            {
                Name = consignmentContract.UKTrader.Name,
                Country = uKTraderCountry,
                Postcode = consignmentContract.UKTrader.PostCode,
                AddressLine1 = consignmentContract.UKTrader.AddressLine1,
                AddressLine2 = consignmentContract.UKTrader.AddressLine2,
                Town = consignmentContract.UKTrader.Town,
                EORINumber = consignmentContract.UKTrader.EORI,
                PaymentType = ukPaymentType,
                DefermentNumber = consignmentContract.UKTrader.DefermentNumber,
                IsCreatedFromAPI = true,
                BranchIdentifier = consignmentContract.UKTrader.BranchIdentifier
            }.GetCompanyForApi();

            if (consignmentContract.ImportedUkTrader == null)
                throw new ValidationException("UKTrader deferment number is not valid.");

            consignmentContract.ImportedConsignment.Partner = consignmentContract.ImportedPartner = await new Company
            {
                Name = consignmentContract.Partner.Name,
                Country = partnerCountry,
                Postcode = consignmentContract.Partner.PostCode,
                AddressLine1 = consignmentContract.Partner.AddressLine1,
                AddressLine2 = consignmentContract.Partner.AddressLine2,
                Town = consignmentContract.Partner.Town,
                EORINumber = consignmentContract.Partner.EORI,
                PaymentType = partnerPaymentType,
                DefermentNumber = consignmentContract.Partner.DefermentNumber,
                IsCreatedFromAPI = true,
                BranchIdentifier = consignmentContract.Partner.BranchIdentifier

            }.GetCompanyForApi();

            if (consignmentContract.ImportedPartner == null)
                throw new ValidationException("Partner deferment number is not valid.");

            consignmentContract.ImportedConsignment.Declarant = consignmentContract.ImportedDeclarant = await new Company
            {
                Name = consignmentContract.Declarant.Name,
                Country = declarantCountry,
                Postcode = consignmentContract.Declarant.PostCode,
                AddressLine1 = consignmentContract.Declarant.AddressLine1,
                AddressLine2 = consignmentContract.Declarant.AddressLine2,
                Town = consignmentContract.Declarant.Town,
                EORINumber = consignmentContract.Declarant.EORI,
                PaymentType = declarantPaymentType,
                DefermentNumber = consignmentContract.Declarant.DefermentNumber,
                IsCreatedFromAPI = true,
                BranchIdentifier = consignmentContract.Declarant.BranchIdentifier

            }.GetCompanyForApi();

            if (consignmentContract.ImportedDeclarant == null)
                throw new ValidationException("Declarant deferment number is not valid.");

        }


        public async static Task MapCarrier(ShipmentContract shipmentContract, Company company)
        {
            var carirer = shipmentContract.Carrier;
            if (carirer != null)
            {
                var carirerCountry = await Country.FindByCodeOrName(carirer.CountryCode, carirer.CountryName, "Carirer");

                if (carirer.CountryCode == "GB" && carirer.PostCode.IsEmpty())
                    throw new ValidationException("Carrier Postcode must have a value.");

                shipmentContract.ImportedCarrier = await new Carrier
                {
                    Name = carirer.Name,
                    Country = carirerCountry,
                    Postcode = carirer.PostCode,
                    AddressLine1 = carirer.AddressLine1,
                    AddressLine2 = carirer.AddressLine2,
                    Town = carirer.Town,
                    EORINumber = carirer.EORI,
                    IsCreatedFromAPI = true,
                    Company = company

                }.GetCarrierForApi();
            }
        }

        public async static Task MapCommodityContract(CommodityContract commodityContract, Consignment consignment, Company company, ConsignmentContract consignmentContract, bool isIntoUK = false, bool safetyAndSecurity = false)
        {
            var countryOfDestination = await Country.FindByCodeOrName(commodityContract.CountryCodeOfDestination, commodityContract.CountryNameOfDestination, "Country Of Destination");
            var preferenceType = await Database.FirstOrDefault<PreferenceType>(x => x.Name == commodityContract.PreferenceType);
            var hasPreference = commodityContract.HasPreference;
            var preferenceCertificateNumber = string.Empty;

            if (hasPreference && preferenceType == null) { throw new ValidationException("Invalid PreferenceType or does not exist."); }
            if (hasPreference && preferenceType != null && preferenceType == PreferenceType.PreferenceCertificateNumber)
            {
                if (commodityContract.PreferenceCertificateNumber.IsEmpty())
                { throw new ValidationException("PreferenceCertificateNumber must have value."); }
                else { preferenceCertificateNumber = commodityContract.PreferenceCertificateNumber; }
            }

            if (((decimal)commodityContract.GrossWeight) < ((decimal)commodityContract.NetWeight))
                throw new ValidationException("Gross mass must be greater than Net mass.");

            if (commodityContract.UNCode.HasValue() && safetyAndSecurity)
            {
                if (!await Commodity.HasUNCode(commodityContract.UNCode))
                    throw new ValidationException($"UNCode: {commodityContract.UNCode} is not in our system, please add valid one");
            }

            commodityContract.ImportedCommodity = new Commodity
            {
                Consignment = consignment,
                GrossWeight = commodityContract.GrossWeight,
                NetWeight = commodityContract.NetWeight,
                Value = commodityContract.Value,
                NumberOfPackages = commodityContract.NumberOfPackages ?? 0,
                CountryOfDestination = countryOfDestination,
                HasPreference = hasPreference,
                PreferenceType = preferenceType,
                PreferenceCertificateNumber = preferenceCertificateNumber,
                GoodsLicencable = commodityContract.IsLicencable,
                SecondQuantity = commodityContract.SecondQuantity,
                ThirdQuantity = commodityContract.ThirdQuantity,
                UNCode = commodityContract.UNCode,
                PHYTODocumentNumber = commodityContract.PHYTO,
                IPAFFDocumentNumber = commodityContract.IPAFF,
                NeedPHYTODocumentNumber = commodityContract.PHYTO.HasValue(),
                NeedIPAFFDocumentNumber = commodityContract.IPAFF.HasValue()
            };

            await MapProducts(commodityContract, company, consignmentContract, isIntoUK);
        }


        public async static Task MapProducts(CommodityContract commodityContract, Company company, ConsignmentContract consignmentContract = null, bool isIntoUK = false)
        {
            var commodityCode = await CommodityCode.FindByExportCodeAndImportCode(commodityContract.Product.CommodityExportCode, commodityContract.Product.CommodityImportCode);

            if (commodityCode == null)
                throw new ValidationException($"{commodityContract.Product.CommodityExportCode + "-" + commodityContract.Product.CommodityImportCode } Invalid Commodity Code or not does exist.");


            var vats = await commodityCode.MultipleVAT;
            var vat = new VATType();

            if (vats.HasMany())
            {
                if (commodityContract.VATRate.IsEmpty())
                {
                    vat = vats.FirstOrDefault(x => x.Name == "S");
                    vat = vat ?? vats.FirstOrDefault(x => x.Name == "A");
                    vat = vat ?? vats.FirstOrDefault(x => x.Name == "Z");
                }
                else
                {
                    if (await commodityCode.MultipleVAT.None(x => x.Name == commodityContract.VATRate))
                        throw new ValidationException($"VAT rate {commodityContract.VATRate} is not availble for commodity {commodityContract.Product.CommodityExportCode}.");
                    vat = await commodityCode.MultipleVAT.FirstOrDefault(x => x.Name == commodityContract.VATRate);
                }
            }
            else if (vats.IsSingle())
            {
                vat = vats.FirstOrDefault();
            }
            else
                throw new ValidationException($"There is no VAT Type for commodity code {commodityContract.Product.CommodityExportCode}.");

            if (vat == null)
                throw new ValidationException($"Please provide VAT Rate commodity code {commodityContract.Product.CommodityExportCode} there is not default VAT Type is available.");

            commodityContract.ImportedCommodity.VAT = vat;
            commodityContract.ImportedCommodity.Product = commodityContract.ImportedProduct = await new Product
            {
                Name = commodityContract.Product.Name,
                Company = company,
                CommodityCode = commodityCode,
                VAT = vat,
                IsCreatedFromAPI = true,
            }.GetProductForApi();
        }

        public static async Task<CPC> GetCompanyCPC(string cpcNumber, Company company)
        {
            var objCPC = await Database.FirstOrDefault<CPC>(x => x.Number == cpcNumber && !x.IsDeactivated);
            var companySpecialCPCLink = await Database.FirstOrDefault<CompanySpecialCPCLink>(x => x.CompanyId == company
            && x.CPCId == objCPC && !x.IsDeactivated);
            return companySpecialCPCLink?.CPC ?? throw new ValidationException("Invalid Company Special CPC or Company Special CPC does not exist.");

        }

        public static string Base64Encode(string text)
        {
            var plainTextBytes = Encoding.ASCII.GetBytes(text);
            return Convert.ToBase64String(plainTextBytes);
        }

        public async static Task MapBorderCrossingAndOfficeDestination(ShipmentContract shipmentContract)
        {
            var officeOfDestinationNCTSCode = await TransitOffice.FindByNCTSCode(shipmentContract.OfficeOfDestinationNCTSCode);
            var secondBorderCrossingNCTSCode = await TransitOffice.FindByNCTSCode(shipmentContract.SecondBorderCrossingNCTSCode);
            var thirdBorderCrossingNCTSCode = await TransitOffice.FindByNCTSCode(shipmentContract.ThirdBorderCrossingNCTSCode);
            var fourthBorderCrossingNCTSCode = await TransitOffice.FindByNCTSCode(shipmentContract.FourthBorderCrossingNCTSCode);

            if (officeOfDestinationNCTSCode == null && shipmentContract.OfficeOfDestinationNCTSCode.HasValue())
                throw new ValidationException("Invalid NCTS code for OfficeOfDestinationNCTSCode or does not exists.");
            if (secondBorderCrossingNCTSCode == null && shipmentContract.SecondBorderCrossingNCTSCode.HasValue())
                throw new ValidationException("Invalid NCTS code for secondBorderCrossingNCTSCode or does not exist.");
            if (thirdBorderCrossingNCTSCode == null && shipmentContract.ThirdBorderCrossingNCTSCode.HasValue())
                throw new ValidationException("Invalid NCTS code for thirdBorderCrossingNCTSCode or does not exist.");
            if (fourthBorderCrossingNCTSCode == null && shipmentContract.FourthBorderCrossingNCTSCode.HasValue())
                throw new ValidationException("Invalid NCTS code for fourthBorderCrossingNCTSCode or does not exist.");


            shipmentContract.ImportedShipment.SecondBorderCrossing = secondBorderCrossingNCTSCode;
            shipmentContract.ImportedShipment.ThirdBorderCrossing = thirdBorderCrossingNCTSCode;
            shipmentContract.ImportedShipment.FourthBorderCrossing = fourthBorderCrossingNCTSCode;
            shipmentContract.ImportedShipment.OfficeOfDestination = officeOfDestinationNCTSCode;
        }

        private static async Task<PaymentType> GetPaymentType(string paymentCode)
        {
            if (paymentCode.HasValue())
            {
                var paymentType = await PaymentType.FindByCode(paymentCode);

                if (paymentType == null)
                    throw new ValidationException("Payment type is not found.");

                return paymentType;
            }

            return null;
        }
    }
}
