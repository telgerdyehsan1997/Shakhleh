using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using Olive;
using Olive.Entities;
using System.Threading;

namespace APIContracts
{
    public class ShipmentContract : IValidatableObject
    {
        internal Shipment ImportedShipment;

        internal List<UploadAttachment> ImportedAttachments;

        [Required]
        public bool IsIntoUK { get; set; }

        [MaxLength(16)]
        [Required]
        public string CustomerReference { get; set; }

        [MaxLength(13)]
        public string VehicleNumber { get; set; }

        [MaxLength(13)]
        public string TrailerNumber { get; set; }

        [Required]
        public DateTime ExpectedDate { get; set; }

        public string PortCode { get; set; }

        public string RouteUKPortCode { get; set; }
        public string RouteNonUKPortCode { get; set; }

        public string OfficeOfDestinationNCTSCode { get; set; }

        public string SecondBorderCrossingNCTSCode { get; set; }

        public string ThirdBorderCrossingNCTSCode { get; set; }

        public string FourthBorderCrossingNCTSCode { get; set; }

        [Required]
        public bool IsDraft { get; set; }

        public bool SafetyAndSecurity { get; set; }

        public bool IsUnaccompanied { get; set; }

        public Carrier Carrier { get; set; }

        internal Domain.Carrier ImportedCarrier;


        //internal TransitOffice ImportedSecondBorderCrossing;
        //internal TransitOffice ImportedThirdBorderCrossing;
        //internal TransitOffice ImportedFourthBorderCrossing;
        //internal TransitOffice ImportedOfficeDestination;

        public bool UseAuthorisedLocation { get; set; }
        public string AuthorisedLocationCustomsIdentity { get; set; }

        public List<AttachmentContract> AttachmentContracts { get; set; } = new List<AttachmentContract>();

        public List<ConsignmentContract> Consignments { get; set; } = new List<ConsignmentContract>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VehicleNumber.IsEmpty() && TrailerNumber.IsEmpty())
                yield return new ValidationResult("VehicleNumber or TrailerNumber must have value.");

            //if (IsIntoUK && PortCode.IsEmpty())
            //    yield return new ValidationResult("PortCode must have value.");

            if (ExpectedDate.IsBefore(LocalTime.Now.Date))
                yield return new ValidationResult("Expected date can't be in the past.");

            //if (!IsIntoUK && IsNCTSShipment)
            //{
            //    if (RouteUKPortCode.IsEmpty())
            //    {
            //        yield return new ValidationResult("RouteUKPortCode Code must have value.");
            //    }
            //    if (RouteNonUKPortCode.IsEmpty())
            //    {
            //        yield return new ValidationResult("RouteNonUKPortCode Code must have value.");
            //    }
            //}

            if (PortCode.IsEmpty() && (RouteUKPortCode.IsEmpty() || RouteNonUKPortCode.IsEmpty()))
                yield return new ValidationResult("PortCode or (RouteUKPortCode and RouteNonUKPortCode) any one of them are required.");


            var regex = new Regex("^[a-zA-Z0-9]*$");

            if (VehicleNumber.HasValue() && !regex.IsMatch(VehicleNumber))
                yield return new ValidationResult("VehicleNumber must be alphanumeric.");
            if (TrailerNumber.HasValue() && !regex.IsMatch(TrailerNumber))
                yield return new ValidationResult("TrailerNumber must be alphanumeric.");


            if (UseAuthorisedLocation && AuthorisedLocationCustomsIdentity.IsEmpty())
                yield return new ValidationResult("AuthorisedLocation must have value if UseAuthorisedLocation is true.");

            if (!UseAuthorisedLocation)
                AuthorisedLocationCustomsIdentity = string.Empty;

            var commodities = Consignments.SelectMany(t => t.CommodityContracts);

            var products = commodities.Select(t => t.Product);

            if (IsIntoUK && products.Any(t => t.CommodityImportCode.IsEmpty() || t.CommodityExportCode.IsEmpty()))
                yield return new ValidationResult("CommodityImportCode and CommodityExportCode must have value.");

            else if (!IsIntoUK && products.Any(t => t.CommodityExportCode.IsEmpty()))
                yield return new ValidationResult("CommodityExportCode must have value.");
        }
    }
}
