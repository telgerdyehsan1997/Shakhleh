using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class NCTSShipment
    {
        public string TrackingNumber { get; set; }
        public string IsBulkShipment { get; set; }
        public string CompanyName { get; set; }
        public string PrimaryContact { get; set; }
        public string Consignor { get; set; }
        public string Consignee { get; set; }
        public string NotifyAdditionalParty { get; set; }
        public string Group { get; set; }
        public string ContactName { get; set; }
        public string CustomerReference { get; set; }
        public string VehicleNumber { get; set; }
        public string TrailerNumber { get; set; }
        public string ExpectedDateOfDeparture { get; set; }
        public string Route { get; set; }
        public string OfficeOfDestination { get; set; }

        public string UseAuthorisedLocation { get; set; }
        public string AuthorisedLocation { get; set; }
        public string GuaranteeLength { get; set; }
        public string UploadAttachments { get; set; }
    }
}
