namespace Domain.AEB.DTOs
{
    public class InvoiceDTO : BaseDTO
    {
        public string InvoiceIdClientSystem { get; set; }
        public string InvoiceNumber { get; set; }
        public DateAndZoneDTO InvoiceDate { get; set; }
        public AmountOfMoneyDTO InvoicePrice { get; set; }
        //public CurrencyRateDTO InvoicePriceRate { get; set; }
    }
}