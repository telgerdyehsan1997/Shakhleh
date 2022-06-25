using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentForMajimaConstruction_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();

            Run<AddKazumaKiryuToMajimaConstruction, ImportCommodityCode, AddRouteBlackpoolAndCalais, AddProductForMajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Creates New Shipment
            ClickLink("New Shipment");

            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");

            //Sets the Details for the Shipment
            this.ClickAndWait("Company name", shipment.CompanyName);
            AtLabel("Type").ClickLabel(shipment.Type);
            AtLabel("NCTS").ClickLabel(shipment.IsNCTS);
            this.ClickAndWait("Route", shipment.Route);
            AtLabel("Safety And Security").ClickLabel(shipment.IsSafetyAndSecurity);
            Set("Primary contact").To("");
            ClickHeader("Shipment Details");
            this.ClickAndWait("Primary contact", shipment.PrimaryContact);
            Set("Customer Reference").To(shipment.CustomerReference);
            Set("Vehicle number").To(shipment.VehicleNumber);
            Set("Trailer number").To(shipment.TrailerNumber);
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