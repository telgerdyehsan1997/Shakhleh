using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class DeliveryItemBrokerDTO : BaseDTO
    {
        public List<ExtraFieldDTO> ExtraFields { get; set; }
    }
}