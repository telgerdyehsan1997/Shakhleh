using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateDraftShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";
            var primaryContact = "Jack Smith";
            var customerRef = "CUSTOMERREF1";
            var vehicleNumber = "123456";
            var trailerNumber = "7890";
            var dateOfArrival = "03/08/2021";
            var shipmentRoute = "CALAIS to Blackpool";
            var trackingNumber = "R0721000001";

            Run<AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to new Shipment
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");

            //Input Shipment Details
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, shipmentRoute);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, shipmentRoute);

            AtField("Primary contact").Expect(primaryContact);
            Set("Customer Reference").To(customerRef);
            Set("Vehicle number").To(vehicleNumber);
            Set("Trailer number").To(trailerNumber);

            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type(dateOfArrival);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            ExpectHeader("Consignment Details");

            //Assert that Shipment has been saved
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            this.FindShipment(trackingNumber);
        }
    }
}