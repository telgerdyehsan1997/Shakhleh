using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class PackageDTO : BaseDTO
    {
        public string PackageIdClientSystem { get; set; }
        public int? Quantity { get; set; }
        //public string TypeCode { get; set; }
        public string TypeUneceCode { get; set; }
        public QuantityDTO GrossMass { get; set; }
        //public QuantityDTO NetMass { get; set; }
        //public string Marks { get; set; }
        public TextInLanguageDTO MarksAndNumbers { get; set; }
        //public List<SealDTO> Seals { get; set; }
        public List<PackedItemDTO> PackedItems { get; set; }
    }
}