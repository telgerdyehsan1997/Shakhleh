using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminManuallyUpdatesShipmentStatusToProcessingError : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";
            var consignmentNumber = "T072100000101";

            Run<AddConsignmentToMajimaConstruction_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Clicks into the Tracking Number 
            AtRow(trackingNumber).Column("Tracking number").ClickLink(trackingNumber);

            ExpectHeader("Shipment Details");
            ExpectRow(consignmentNumber);

            //Updates the Status of the Consignment
            AtRow(consignmentNumber).Column("Progress").Click("Draft");
            ExpectHeader("Progress History");
            Set("Progress").To("ProcessingErrorArrival");
            ClickButton("Save");

            //Asserts that Progress has been updated
            AtRow(consignmentNumber).Column("Progress").Expect("Processing Error");
        }
    }
}