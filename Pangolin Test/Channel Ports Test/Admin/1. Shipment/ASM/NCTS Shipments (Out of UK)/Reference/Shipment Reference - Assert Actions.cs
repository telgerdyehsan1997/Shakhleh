using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ShipmentReference_AssertActions : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";
            var consignmentNumber = "T072100000101";

            Run<AddCommoditiesToSafetyAndSecurityConsignment>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Shipments Reference
            AtRow(trackingNumber).Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");
            ExpectRow(consignmentNumber);

            //Asserts that the Actions columnn has been added
            ExpectColumn("Actions");

            //Asserts the current Actions that can be taken
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            Expect(What.Contains, "View Logs");
            Expect("View EAD Document");
            Expect("Edit");
        }
    }
}