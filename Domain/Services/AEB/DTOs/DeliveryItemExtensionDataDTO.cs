namespace Domain.AEB.DTOs
{
    public class DeliveryItemExtensionDataDTO : BaseDTO
    {
        public DeliveryItemEUDTO ExportBE { get; set; }
        public DeliveryItemEUDTO ExportDK { get; set; }
        public DeliveryItemEUDTO ExportFR { get; set; }
        public DeliveryItemEUDTO ExportGB { get; set; }
        public DeliveryItemEUDTO ExportNL { get; set; }
        public DeliveryItemEUDTO ExportPL { get; set; }
        public DeliveryItemEUDTO ExportSE { get; set; }
        public DeliveryItemBrokerDTO ExportBroker { get; set; }
        public DeliveryItemBrokerDTO ImportBroker { get; set; }
    }
}