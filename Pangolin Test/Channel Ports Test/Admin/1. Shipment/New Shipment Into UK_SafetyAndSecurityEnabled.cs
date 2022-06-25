using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewShipmentIntoUK_SafetyAndSecurityEnabled : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().MajimaConstrunctionSafetyAndSecurityIntoUK();

            Run<AddKazumaKiryuToMajimaConstruction, AddCompanyUserToMajimaConstructionMajima, AddProductForMajimaConstruction, AddRouteBlackpoolAndCalais, SetHealthCertificateCode, NewCarrier_Amazon>();
            LoginAs<ChannelPortsAdmin>();

            //Creates new Shipment
            ClickLink("New Shipment");

            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");

            //Adds Shipment Details
            this.ClickAndWait("Company name", shipment.CompanyName);
            System.Threading.Thread.Sleep(1000);
            AtLabel("Type").ClickLabel(shipment.Type);
            this.ClickAndWait("Route", shipment.Route);
            AtLabel("Safety and security").ClickLabel(shipment.IsSafetyAndSecurity);
            this.ClickAndWait("Primary contact", shipment.PrimaryContact);
            Set("Customer Reference").To(shipment.CustomerReference);
            Set("Vehicle number").To(shipment.VehicleNumber);
            AtLabel("Unaccompanied").ClickLabel(shipment.IsUnaccompanied);
            this.ClickAndWait("Carrier", shipment.Carrier);
            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type(shipment.ExpectedDate);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");
            ExpectHeader("Consignment Details");

            //Asserts that Shipment has been saved
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            this.FindShipment(shipment.TrackingNumber);
        }
    }
}