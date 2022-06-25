using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertSearchFilterActiveTextVisible : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<AddShipmentIntoUKASMAccepted>();

            LoginAs<ChannelPortsAdmin>();

            //Searches for the Shipment
            Set("Tracking number").To(trackingNumber);
            ClickButton("Search");

            //Checks if Active Search Filter text is present
            ExpectRow(trackingNumber);
            Expect(What.Contains, "Shipment Level Search Filter: Active");

            //Checks if Active Search Filter text no longer appears after Search is cleared
            ClickButton("Clear Shipment Level Search");
        }
    }
}