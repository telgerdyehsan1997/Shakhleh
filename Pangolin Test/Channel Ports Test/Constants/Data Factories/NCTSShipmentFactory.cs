using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class NCTSShipmentFactory
    {
        private NCTSShipment _NCTSMajimaConstruction { get; set; }
        public NCTSShipment CreateNCTSShipmentMajimaConstruction()
        {
            _NCTSMajimaConstruction = _NCTSMajimaConstruction ?? new NCTSShipment()
            {
                TrackingNumber = "1000000",
                IsBulkShipment = "No",
                CompanyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567",
                PrimaryContact = "KAZUMA KIRYU",
                CustomerReference = "AuthorisedNCTS",
                VehicleNumber = "A112",
                ExpectedDateOfDeparture = "01/07/2021",
                Route = "Blackpool to CALAIS",
                OfficeOfDestination = "IT IT IT112345 ITALY",
                UseAuthorisedLocation = "Yes",
                AuthorisedLocation = "Warehouse 1",
                GuaranteeLength = "8"
            };
            return _NCTSMajimaConstruction;
        }
    }
}
