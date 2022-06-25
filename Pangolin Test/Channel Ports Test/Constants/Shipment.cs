using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class Shipment
    {
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string IsNCTS { get; set; }
        public string Route { get; set; }
        public string IsSafetyAndSecurity { get; set; }
        public string PrimaryContact { get; set; }
        public string NotifyAdditionalParties { get; set; }
        public string Group { get; set; }
        public string ContactName { get; set; }
        public string CustomerReference { get; set; }
        public string VehicleNumber { get; set; }
        public string TrailerNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string ExpectedDate { get; set; }
        public string UploadAttachments { get; set; }
        public string TrackingNumber { get; set; }
        public string IsUnaccompanied { get; set; }
        public string Carrier { get; set; }
    }
}
