using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Olive;

namespace APIContracts
{
    public class ConsignmentContract : IValidatableObject
    {
        internal Consignment ImportedConsignment;

        public UKTrader UKTrader { get; set; }

        public Partner Partner { get; set; }

        public Declarant Declarant { get; set; }

        internal Company ImportedUkTrader;

        internal Company ImportedPartner;

        internal Company ImportedDeclarant;

        public string SpecialCPC { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Total Packages must be greater than 0")]
        public int TotalPackages { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Total Gross Weight must be greater than 0")]
        public double TotalGrossWeight { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Total Net Weight must be greater than 0")]
        public double TotalNetWeight { get; set; }

        [Required]
        public string InvoiceNumber
        {
            get; set;
        }

        public string SecondInvoiceNumber { get; set; }
        public string ThirdInvoiceNumber { get; set; }
        public string FourthInvoiceNumber { get; set; }

        [Required]
        public string InvoiceCurrency { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Total Value must be greater than 0")]
        public decimal TotalValue { get; set; }

        public string TermsOfSale { get; set; }

        public string UCN { get; set; }

        //internal TermOfSale ImportedTermsOfSale;

        public string FreightCurrency { get; set; }
        public decimal FreightAmount { get; set; }
        public string DDPOption { get; set; }
        //internal DDPType ImportedDDPType;
        public decimal Box63NonEUPercent { get; set; }

        public string InsuranceCurrency { get; set; }
        public decimal InsuranceAmount { get; set; }

        //[Required]
        //public string UCR{ get; set; }

        public string LRN { get; set; }

        public List<CommodityContract> CommodityContracts { get; set; } = new List<CommodityContract>();


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FreightCurrency.IsEmpty() && FreightAmount > 0m)
                yield return new ValidationResult("FreightCurrency and FreightAmount must have value or both empty.");

            if (FreightCurrency.IsEmpty() && Box63NonEUPercent > 0m)
                yield return new ValidationResult("FreightCurrency and Box63NonEUPercent must have value or both empty.");

            if (InsuranceCurrency.IsEmpty() && InsuranceAmount > 0m)
                yield return new ValidationResult("InsuranceCurrency and InsuranceAmount must have value or both empty.");

            if (FreightAmount < 0m)
                FreightAmount = 0m;

            if (InsuranceAmount < 0m)
                InsuranceAmount = 0m;
        }
    }
}
