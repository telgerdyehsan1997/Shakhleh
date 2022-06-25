namespace Domain.AEB.DTOs
{
    public class ProducedDocumentEUDTO : BaseDTO
    {
        public string TypeCode { get; set; }
        public string Reference { get; set; }
        public DateAndZoneDTO InformationDate { get; set; }
        public QuantityDTO Quantity { get; set; }
        public AmountOfMoneyDTO Amount { get; set; }
        public string ComplementaryInfo { get; set; }
        public string StatusCode { get; set; }
        public string DocumentPart { get; set; }
    }
}