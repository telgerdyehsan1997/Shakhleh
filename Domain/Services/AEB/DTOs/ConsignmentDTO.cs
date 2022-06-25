using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AEB.DTOs
{
    public class ConsignmentDTO : BaseDTO
    {
        public string ConsignmentIdClientSystem { get; set; }
        public string OrganizationUnitClientSystem { get; set; }
        public string ConsignmentNumber { get; set; }
        public string Remark { get; set; }
        public string ProfileCode { get; set; }
        public PersonDTO PersonInCharge { get; set; }
        public List<DeliveryDTO> Deliveries { get; set; }
        //public List<CostDTO> Costs { get; set; }
    }
}
