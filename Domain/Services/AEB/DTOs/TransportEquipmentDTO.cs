using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class TransportEquipmentDTO : BaseDTO
    {
        public string EquipmentType { get; set; }
        public string Identification { get; set; }
        public List<SealDTO> Seals { get; set; }
    }
}