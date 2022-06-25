using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class DeliveryItemDTO : BaseDTO
    {
        public string ItemIdClientSystem { get; set; }
        public string ItemNumber { get; set; }
        public string MaterialNumber { get; set; }
        public string InvoiceIdClientSystem { get; set; }
        public QuantityDTO GrossMass { get; set; }
        public QuantityDTO NetMass { get; set; }
        //public AmountOfMoneyDTO NetPrice { get; set; }
        //public string OriginCountryCode { get; set; }
        //public string PreferentialOriginCountryCode { get; set; }
        //public string DispatchCountryCode { get; set; }
        //public string DestinationCountryCode { get; set; }
        //public bool? IsEligiblePreference { get; set; }
        public AmountOfMoneyDTO StatisticalValue { get; set; }
        //public string AdditionalAggregationKey { get; set; }
        //public string AdditionalSplitCriteria { get; set; }
        //public DeliveryItemExtensionDataDTO Extension { get; set; }
        public List<ClassificationDTO> Classifications { get; set; }
        public List<TextInLanguageDTO> GoodsDescription { get; set; }
        //public List<PartyDTO> Parties { get; set; }
        //public List<CostDTO> Costs { get; set; }
        public List<QuantityDTO> Quantities { get; set; }
        //public List<AdditionalReferenceDTO> AdditionalReferences { get; set; }
        public List<CustomsProcedureDTO> CustomsProcedures { get; set; }
    }
}