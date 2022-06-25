using Domain;
using Olive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIContracts
{
    public class Guarantor 
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string PostCode { get; set; }
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string EORI { get; set; }
        public string PaymentCode { get; set; }
        public string DefermentNumber { get; set; }
        public bool IsDirectRepresentation { get; set; } = true;
    }
}
