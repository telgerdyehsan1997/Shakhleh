using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewShipment_AssertRouteFieldShowsCorrectPorts : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";

            Run<AddRoutePortsmouthToAmsterdam>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipments
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");

            //Adds the Company and changes the Shipment Type to be Into the UK
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            //Checks the Route to see if it shows the Non UK Port first as the Shipment is coming into the UK
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "AMSTERDAM to Portsmouth");

            Set("Route").To("");
            AtLabel("Type").ClickLabel("Out of UK");
            System.Threading.Thread.Sleep(1000);

            //Checks the Route to see if it shows the UK Port first as the Shipment is going out of the UK
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to AMSTERDAM");
        }
    }
}