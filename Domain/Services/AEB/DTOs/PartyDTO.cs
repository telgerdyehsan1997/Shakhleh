using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class PartyDTO : BaseDTO
    {
        public string PartyType { get; set; }
        public string CustomsProcess { get; set; }
        public string CompanyNumber { get; set; }
        public string Name { get; set; }
        //public string Name2 { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        //public string District { get; set; }
        public string Country { get; set; }
        //public PersonDTO Contact { get; set; }
        //public bool? InitFromCompanyMasterFileData { get; set; }
        public List<CustomsIdentifcationDTO> CustomsIds { get; set; }
    }
}