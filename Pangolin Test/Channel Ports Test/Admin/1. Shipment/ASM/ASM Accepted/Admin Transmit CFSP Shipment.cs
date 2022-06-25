using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminTransmitCFSPShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<AdminAddsCommoditiesToCFSPConsignment_SequenceNumber>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to Commodities
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink();

            //Completes the Shipment
            Expect("Complete");
            Click("Complete");
            ExpectRow(consignmentNumber);

            //Navigates to Shipment Details
            ClickLink("Shipments");
            ExpectRow(trackingNumber);
            AtRow(trackingNumber).Column("Tracking number").ClickLink();
           
            //Transmits the Shipment
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            Expect("Transmit to HRMC");
            System.Threading.Thread.Sleep(1000);
            Click("Transmit to HRMC");

            //Views the Logs for the Shipment
            RefreshPage();
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            Expect(What.Contains, "View Logs");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");
            ExpectHeader("Logs");
            ExpectRow("CreateASMDeclarationRequest");
            AtRow("CreateASMDeclarationRequest").Column("File").Click("Download");
            ExpectHeader("Logs");

            //Manually check to see if the DUCR contains the Sequence number and the Companies Authorisation Number
        }
    }
}