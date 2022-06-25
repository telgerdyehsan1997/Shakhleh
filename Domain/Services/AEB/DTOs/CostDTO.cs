namespace Domain.AEB.DTOs
{
    public class CostDTO : BaseDTO
    {
        public string CostType { get; set; }
        public AmountOfMoneyDTO Value { get; set; }
    }
}