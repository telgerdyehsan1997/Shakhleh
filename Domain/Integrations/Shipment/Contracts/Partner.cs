using Domain;
using Olive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIContracts
{
    public class Partner 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        [Required]
        public string Town { get; set; }
        public string EORI { get; set; }
        public string PaymentCode { get; set; }
        public string DefermentNumber { get; set; }
        public bool IsDirectRepresentation { get; set; } = true;
        public string BranchIdentifier { get; set; }


    }
}
