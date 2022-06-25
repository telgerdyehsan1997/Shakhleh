using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSShipmentReference_AssertActions : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000000";
            var lrnNumber = "CP100000001";

            Run<AddDraftConsignmentToNCTSShipment>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            ClickLink("NCTS Shipments Out of UK");
            this.FindNCTSShipment(trackingNumber);

            //Navigates to the Shipments Reference
            AtRow(trackingNumber).Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");
            ExpectRow(lrnNumber);

            //Asserts that the Actions columnn has been added
            ExpectColumn("Actions");

            //Asserts the current Actions that can be taken
            AtRow(lrnNumber).Column("Actions").Click("Select action");
            Expect(What.Contains, "View Logs");
            Expect("Download Transit Document");
        }
    }
}