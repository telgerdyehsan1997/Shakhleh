using Domain;
using Olive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIContracts
{
    public class UKTrader : IValidatableObject
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


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CountryCode == "GB" && EORI.IsEmpty())
                yield return new ValidationResult(@"EORI number is required for " + Name);

            if(EORI == Constants.ChannelPortEORI)
                yield return new ValidationResult(@"You can't use ChannelPorts EORI for UKTrader.");
        }
    }
}
