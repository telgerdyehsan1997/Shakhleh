namespace Domain.AEB.DTOs
{
    public class QuantityDTO : BaseDTO
    {
        public decimal? Value { get; set; }
        public string Unit { get; set; }
    }
}