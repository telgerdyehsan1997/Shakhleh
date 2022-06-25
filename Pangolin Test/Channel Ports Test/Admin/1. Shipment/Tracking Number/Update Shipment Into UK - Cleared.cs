using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UpdateShipmentIntoUK_Cleared : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<MajimaConstruction_TransmitShipmentIntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Clicks into the Tracking Number 
            AtRow(trackingNumber).Column("Tracking number").ClickLink(trackingNumber);

            ExpectHeader("Shipment Details");
            ExpectRow(consignmentNumber);

            //Updates the Status of the Consignment
            AtRow(consignmentNumber).Column("Progress").Click("Ready to Transmit");
            ExpectHeader("Progress History");
            Set("Progress").To("Cleared");
            ClickButton("Save");

            //Asserts that Progress has been updated
            AtRow(consignmentNumber).Column("Progress").Expect("Cleared");
        }
    }
}