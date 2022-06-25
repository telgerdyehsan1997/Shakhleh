namespace Domain.AEB.DTOs
{
    public class PackedItemDTO : BaseDTO
    {
        public string ItemIdClientSystem { get; set; }
        public decimal? PackedQuantity { get; set; }
    }
}