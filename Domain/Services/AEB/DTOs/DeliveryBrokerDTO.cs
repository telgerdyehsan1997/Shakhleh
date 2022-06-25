using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class DeliveryBrokerDTO : BaseDTO
    {
        public List<ExtraFieldDTO> ExtraFields { get; set; }
    }
}