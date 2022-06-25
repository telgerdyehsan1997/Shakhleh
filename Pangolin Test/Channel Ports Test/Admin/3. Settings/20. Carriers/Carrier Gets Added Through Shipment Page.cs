using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CarrierGetsAddedThroughShipmentPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().MajimaConstrunctionSafetyAndSecurityIntoUK();
            var carrier = new Constants.CarrierFactory().CreateCarrierAmazon();

            Run<AddKazumaKiryuToMajimaConstruction, AddRouteBlackpoolAndCalais>();
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

            //Asserts that the Unaccompanied and Carrier fields are visible
            ExpectLabel("Unaccompanied");
            ExpectField("Carrier");
            Click("AddCarrier");
            ExpectHeader("Carrier Details");

            //Adds the carrier
            Set("Carrier name").To(carrier.Name);
            Set("Address line 1").To(carrier.AddressLine1);
            Set("Address line 2").To(carrier.AddressLine2);
            Set("Town/City").To(carrier.Town);
            Set("Postcode/Zip code").To(carrier.Postcode);
            this.ClickAndWait("Country", carrier.Country);
            Set("EORI number").To(carrier.EORINumber);
            ClickButton("Save");

            //Asserts that the Carrier has been saved
            ExpectHeader("Shipment Details");
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Carriers");

            ExpectRow(carrier.Name);
        }
    }
}