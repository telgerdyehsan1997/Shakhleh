namespace Domain.AEB.DTOs
{
    public class AmountOfMoneyDTO : BaseDTO
    {
        public decimal? Value { get; set; }
        public string CurrencyIso { get; set; }
    }
}