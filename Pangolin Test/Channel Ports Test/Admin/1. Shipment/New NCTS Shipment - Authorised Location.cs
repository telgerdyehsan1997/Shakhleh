using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewNCTSShipment_AuthorisedLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.NCTSShipmentFactory().CreateNCTSShipmentMajimaConstruction();

            Run<AddKazumaKiryuToMajimaConstruction, AddAnAuthorisedLocationToMajimaConstruction, AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments
            ClickLink("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            ClickLink("New NCTS Shipment Out of UK");

            //Creates the new Shipment
            ExpectHeader("Shipment details");
            AtLabel("Is this a bulk shipment?").ClickLabel(shipment.IsBulkShipment);
            this.ClickAndWait("Company name", shipment.CompanyName);
            this.ClickAndWait("Primary contact", shipment.PrimaryContact);
            Set("Customer Reference").To(shipment.CustomerReference);
            Set("Vehicle number").To(shipment.VehicleNumber);
            Set("Expected date of departure").To(shipment.ExpectedDateOfDeparture);
            this.ClickAndWait("Route", shipment.Route);
            this.ClickAndWait("Office of Destination", shipment.OfficeOfDestination);
            AtLabel("Use authorised location").ClickLabel(shipment.UseAuthorisedLocation);
            AtLabel("Authorised location").Expect(shipment.AuthorisedLocation);
            Set("Guarantee length").To(shipment.GuaranteeLength);
            Click(What.Contains, "Save");

            ExpectHeader("Consignment Details");

            //Assert that Shipment has been created
            ClickLink("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            this.FindNCTSShipment(shipment.TrackingNumber);
        }
    }
}