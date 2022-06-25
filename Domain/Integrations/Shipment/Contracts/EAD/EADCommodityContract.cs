using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Olive;


namespace APIContracts
{
    public class CommodityContract : IValidatableObject
    {
        internal Commodity ImportedCommodity;

        internal Product ImportedProduct;

        public ProductContract Product { get; set; }


        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Gross Weight must be greater than 0")]
        public double GrossWeight { get; set; }
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Net Weight must be greater than 0")]
        public double NetWeight { get; set; }

        public double? SecondQuantity { get; set; }

        public double? ThirdQuantity { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public decimal Value { get; set; }

        public int? NumberOfPackages { get; set; }
        [Required]
        public string CountryCodeOfDestination { get; set; }
        [Required]
        public string CountryNameOfDestination { get; set; }

        public bool HasPreference { get; set; }

        public string PreferenceType { get; set; }

        public string PHYTO { get; set; }
        public string IPAFF { get; set; }

        public string PreferenceCertificateNumber { get; set; }

        public bool IsLicencable { get; set; }
        public string VATRate { get; set; }

        public string UNCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Product == null)
                yield return new ValidationResult("ProductCode must have a value.");

        }

    }
}
