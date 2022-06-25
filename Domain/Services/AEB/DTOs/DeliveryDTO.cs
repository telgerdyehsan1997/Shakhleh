using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class DeliveryDTO : BaseDTO
    {
        public string DeliveryIdClientSystem { get; set; }
        public string OrganizationUnitClientSystem { get; set; }
        public string DeliveryNumber { get; set; }
        public string Remark { get; set; }
        public QuantityDTO TotalGrossMass { get; set; }
        public QuantityDTO TotalNetMass { get; set; }
        public string DispatchCountryCode { get; set; }
        public string DestinationCountryCode { get; set; }
        public DateAndZoneDTO DecisiveDate { get; set; }
        public TradeTermsDTO TradeTerms { get; set; }
        //public bool? IsContainerised { get; set; }
        public string IsContainerised { get; set; }
        public List<PartyDTO> Parties { get; set; }
        //public List<TransportEquipmentDTO> TransportEquipments { get; set; }
        public List<CustomsOfficeDTO> CustomsOffices { get; set; }
        public List<TransportMeansDTO> TransportMeans { get; set; }
        //public List<RoutingCountryDTO> Itinerary { get; set; }
        public List<InvoiceDTO> Invoices { get; set; }
        public List<DeliveryItemDTO> Items { get; set; }
        public List<PackageDTO> Packages { get; set; }
        //public DeliveryExtensionDataDTO Extension { get; set; }
        public List<TextInLanguageDTO> GoodsDescription { get; set; }
        //public List<AdditionalReferenceDTO> AdditionalReferences { get; set; }
    }
}