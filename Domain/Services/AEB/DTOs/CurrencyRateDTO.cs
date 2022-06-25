namespace Domain.AEB.DTOs
{
    public class CurrencyRateDTO : BaseDTO
    {
        public decimal? Rate { get; set; }
        public DateAndZoneDTO RateDate { get; set; }
    }
}