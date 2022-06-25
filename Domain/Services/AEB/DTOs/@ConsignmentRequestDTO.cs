using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AEB.DTOs
{
    public class ConsignmentRequestDTO : BaseDTO
    {
        public string ClientSystemId { get; set; }
        public string ClientIdentCode { get; set; }
        public string UserName { get; set; }
        public List<string> ResultLanguageIsoCodes { get; set; }
        public ConsignmentDTO Consignment { get; set; }
    }
}
