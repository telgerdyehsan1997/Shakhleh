namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using Domain.AEB.DTOs;
    using System.Text.RegularExpressions;
    using APIHandler;

    /// <summary>
    /// Provides the business logic for Commodity class
    /// </summary>
    partial class Commodity
    {
        static readonly string AEB_Client_Name = Config.Get<string>("AEBCustomsManagement:ClientName");
        IEORIService EORIService = Context.Current.GetService<IEORIService>();
        const int LENGTH = 20;
        /// <summary>
        /// Throw a validation exception if the Commodity's Value has more than 2 decimal points, or if
        /// the user attempts to create a new Commodity for a Consignment with 99 or more Commodities.
        /// </summary>
        /// 


        public override async Task Validate()
        {
            await base.Validate();

            await Validation();

            var validate = Consignment.IsHaveAllDeclaraionDetails();

            if (validate && ((decimal)GrossWeight).GetNumberOfDecimalPlaces() > 2)
                throw new ValidationException("Gross Weight should only have 2 decimal places.");

            if (validate && ((decimal)NetWeight).GetNumberOfDecimalPlaces() > 3)
                throw new ValidationException("Net Weight should only have 3 decimal places.");

            if (validate && SecondQuantity <= 0 && !Consignment.Shipment.IsAPI)
                throw new ValidationException("Second Quantity should have value more then 0");

            if (SecondQuantity.HasValue)
            {
                if (((decimal)SecondQuantity).GetNumberOfDecimalPlaces() > 3)
                    throw new ValidationException("Second Quantity should only have 3 decimal places.");
            }
            if (ThirdQuantity.HasValue)
            {
                if (((decimal)ThirdQuantity).GetNumberOfDecimalPlaces() > 3)
                    throw new ValidationException("Third Quantity should only have 3 decimal places.");
            }
            if (validate && Value.HasValue && Value.Value.GetNumberOfDecimalPlaces() > 2)
                throw new ValidationException("Value should only have 2 decimal places.");

            if (IsNew && await Consignment.Commodities.GetList().Count() >= Constants.EADShipmentCommodityMax)
                throw new ValidationException($"A consignment may only have up to {Constants.EADShipmentCommodityMax} commodities. Please remove an existing commodity before adding a new one");

            if (AreTheGoodHazardous && !await HasUNCode(UNCode))
                throw new ValidationException($"UNCode: {UNCode} is not in our system, please add valid one");

            if (validate && ((decimal)GrossWeight) < ((decimal)NetWeight))
                throw new ValidationException("Gross mass must be greater than Net mass.");

            var totalgross = (await Consignment.Commodities.Where(x => x.ID != ID).Sum(x => x.GrossWeight) ?? 0) + GrossWeight;
            if (validate && (decimal)totalgross > (decimal)Consignment.TotalGrossWeight && !Consignment.Shipment.IsAPI)
                throw new ValidationException("Consignment total gross weight must be equal to commodities gross weight");

            var totalnet = (await Consignment.Commodities.Where(x => x.ID != ID).Sum(x => x.NetWeight) ?? 0) + NetWeight;
            if (validate && (decimal)totalnet > (decimal)Consignment.TotalNetWeight && !Consignment.Shipment.IsAPI)
                throw new ValidationException("Consignment total net weight must be equal to commodities net weight");

            if (GoodsLicencable && Quantity <= 0 && LicenceType?.Quantity == true)
                throw new ValidationException("Quantity Value must be greater then 0.");
        }

        public async Task Validation()
        {
            var result = new List<string>();
            var validate = Consignment.IsHaveAllDeclaraionDetails();

            if (validate && ProductId == null)
                result.Add("Please provide a value for Product.");

            if (validate && SecondQuantity < 0)
                result.Add("The value of Second quantity must be 0 or more.");

            if (validate && SecondQuantity > 99999999999.999)
                result.Add("The value of Second quantity must be 99999999999.999 or less.");

            if (validate && ThirdQuantity < 0)
                result.Add("The value of Third quantity must be 0 or more.");

            if (validate && ThirdQuantity > 99999999999.999)
                result.Add("The value of Third quantity must be 99999999999.999 or less.");

            if (validate && Value < 0.01m)
                result.Add("The value of Value must be 0.01 or more.");

            if (validate && Value > 9999999999.99m)
                result.Add("The value of Value must be 9999999999.99 or less.");

            if (validate && GrossWeight < 0.01)
                result.Add("The value of Gross weight must be 0.01 or more.");

            if (validate && GrossWeight > 99999999999.99)
                result.Add("The value of Gross weight must be 99999999999.99 or less.");

            if (validate && NetWeight < 0.01)
                result.Add("The value of Net weight must be 0.01 or more.");

            if (validate && NetWeight > 99999999999.999)
                result.Add("The value of Net weight must be 99999999999.999 or less.");

            if (RPTIDCode.HasValue())
                if (validate && !(await IsValidRPTID()) && !(await Consignment.Shipment.Company.IsEORINumberValid(RPTIDCode, CountryOfDestination)))
                    result.Add("The RPTID code must be 6 digits or should follow the format of a valid EORI Number.");

            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }

        internal DeliveryDTO ToDTO()
        {
            return new DeliveryDTO
            {
                DeliveryIdClientSystem = Consignment.ConsignmentNumber,
                OrganizationUnitClientSystem = AEB_Client_Name,
                DeliveryNumber = Consignment.ConsignmentNumber,
                Remark = "Export from ChannelPorts in house system",
                TotalGrossMass = new QuantityDTO { Value = (decimal?)GrossWeight, Unit = "KG" },
                TotalNetMass = new QuantityDTO { Value = (decimal?)NetWeight, Unit = "KG" },
                DispatchCountryCode = "???",
                DestinationCountryCode = "???",
                DecisiveDate = LocalTime.Now.ToDateAndZoneDTO(),
                TradeTerms = new TradeTermsDTO { IncotermCode = "", Place = "", Info = "" },
                IsContainerised = "???",
                Parties = (new List<Company> { Consignment.Partner, Consignment.Declarant }).Select(c => c.ToPartyDTO()).ToList(),
                //TransportEquipments = new List<TransportEquipmentDTO> { },
                CustomsOffices = new List<CustomsOfficeDTO> { new CustomsOfficeDTO { OfficeType = "???", OfficeCode = "???", SequenceNumber = "???" } },
                TransportMeans = new List<TransportMeansDTO> { new TransportMeansDTO { MeansType = "???", TransportModeCode = "???", Identification = "???" } },
                //Itinerary = new List<RoutingCountryDTO> { },
                Invoices = new List<InvoiceDTO>
                {
                    new InvoiceDTO
                    {
                        InvoiceIdClientSystem = AEB_Client_Name,
                        InvoiceNumber = Consignment.InvoiceNumber,
                        InvoiceDate = new DateAndZoneDTO{ DateInTimezone = "???" },
                        InvoicePrice = new AmountOfMoneyDTO{ Value = Consignment.TotalValue, CurrencyIso = Consignment.InvoiceCurrency.Name }
                    }
                },
                Items = new List<DeliveryItemDTO>
                {
                    new DeliveryItemDTO
                    {
                        ItemIdClientSystem = "???",
                        ItemNumber = "???",
                        MaterialNumber = "",
                        InvoiceIdClientSystem = AEB_Client_Name,
                        GrossMass = new QuantityDTO { Value = (decimal?) GrossWeight, Unit = "KG" },
                        NetMass = new QuantityDTO { Value = (decimal?)NetWeight, Unit = "KG" },
                        StatisticalValue = new AmountOfMoneyDTO{ Value = Consignment.TotalValue, CurrencyIso = Consignment.InvoiceCurrency.Name },
                        Classifications = new List<ClassificationDTO>{ new ClassificationDTO { ClassificationType = "???", ClassificationValue = "???" } },
                        GoodsDescription = new List<TextInLanguageDTO>{ new TextInLanguageDTO { LanguageISOCode = "???", Text = "???" } },
                        Quantities = new List<QuantityDTO>{ new QuantityDTO { Value = (decimal?)GrossWeight, Unit = "KG" } },
                        CustomsProcedures = new List<CustomsProcedureDTO>
                        {
                            new CustomsProcedureDTO { CustomsProcedureType = "???", CustomsProcedureCode = "???" },
                            new CustomsProcedureDTO { CustomsProcedureType = "???", CustomsProcedureCode = "???" }
                        }
                    }
                },
                Packages = new List<PackageDTO>
                {
                    new PackageDTO
                    {
                        PackageIdClientSystem = AEB_Client_Name,
                        Quantity = Consignment.TotalPackages,
                        TypeUneceCode = "???",
                        GrossMass = new QuantityDTO{ Value = (decimal?)GrossWeight, Unit = "KG" },
                        MarksAndNumbers= new TextInLanguageDTO { LanguageISOCode="???", Text="???" },
                        PackedItems = new List<PackedItemDTO>{ new PackedItemDTO { ItemIdClientSystem="???", PackedQuantity = Consignment.TotalPackages } }
                    }
                },
                //Extension = new DeliveryExtensionDataDTO { },
                GoodsDescription = new List<TextInLanguageDTO> { new TextInLanguageDTO { LanguageISOCode = "???", Text = "???" } },
                //AdditionalReferences = new List<AdditionalReferenceDTO> { }
            };
        }

        public async Task<decimal> GetGBPInvoice()
        {
            var currency = Consignment.InvoiceCurrency;

            if (currency.Name == "GBP")
                return Value ?? 0;

            return await CurrencyExchangeService.ExchangeToGBP(currency, Value ?? 0, Consignment.Shipment.Date);
        }

        public async Task UpdateComodityDutyPay(Commodity commodity)
        {
            if (Consignment.IsHaveAllDeclaraionDetails() && commodity.HasPreference == false && commodity.Product.CommodityCode.HasFullRateOfDuty && commodity.Product.CommodityCode.HasSpecificRate)
            {
                commodity = await Database.Reload(commodity);
                await Database.Update(commodity, x => x.IsDutyPayable = true, SaveBehaviour.BypassAll);
            }
        }

        [Calculated]
        public bool HasDutyPayable =>
            (CountryOfDestination.PreferenceAvailable == false || (CountryOfDestination.PreferenceAvailable == true && HasPreference == false)) && (Product.CommodityCode.HasFullRateOfDuty || Product.CommodityCode.HasSpecificRate) && (HasPreference.HasValue && !HasPreference.Value);

        public static async Task<bool> HasUNCode(string uNCode)
        {
            var uNNo = await Database.Of<UNCode>().Where(x => x.UNNo == uNCode).FirstOrDefault();
            if (uNNo != null)
                return true;
            return false;
        }
        public async Task<bool> CommoditiesWithApply(IEnumerable<Commodity> commodities, Guid? productId, bool isNew)
        {
            var product = await Database.Of<Product>().Where(x => x.ID == productId).FirstOrDefault();
            commodities = commodities.Where(x => x.IsApplyForAll == true);
            if ((product.CommodityCode?.N852_CED == true && product.CommodityCode?.N851_PHC == true) || (product?.CommodityCode?.N853_CVD == true && product.CommodityCode?.N851_PHC == true))
            {
                if (commodities.Any(x => x.IPAFFDocumentNumber.HasValue()) && commodities.Any(x => x.PHYTODocumentNumber.HasValue()))
                    return false;
                return true;
            }
            else if (product.CommodityCode?.N851_PHC == true)
            {
                var commidity = commodities.Where(x => x.PHYTODocumentNumber.HasValue());
                return (isNew && commidity.Count() == 0) || (!isNew && commidity.Count(x => x.ID == ID) == 1);
            }
            else if (product.CommodityCode?.N852_CED == true || product?.CommodityCode?.N853_CVD == true)
            {
                var commidity = commodities.Where(x => x.IPAFFDocumentNumber.HasValue());
                return (isNew && commidity.Count() == 0) || (!isNew && commidity.Count(x => x.ID == ID) == 1);
            }

            return false;
        }

        async Task<bool> IsValidRPTID()
        {
            if (RPTIDCode.HasValue())
                return RPTIDCode.All(Char.IsDigit) && RPTIDCode.Length == 6;
            return false;
        }
    }
}
