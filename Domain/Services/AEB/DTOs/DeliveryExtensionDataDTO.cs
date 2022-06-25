namespace Domain.AEB.DTOs
{
    public class DeliveryExtensionDataDTO : BaseDTO
    {
        public DeliveryBrokerDTO ExportBroker { get; set; }
        public DeliveryBrokerDTO ImportBroker { get; set; }
    }
}