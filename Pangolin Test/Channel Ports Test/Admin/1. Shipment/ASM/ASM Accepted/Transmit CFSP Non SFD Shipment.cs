using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitCFSPNonSFDShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodityCountry = "GBP";

            Run<AddCommoditiesToCFSPSFDConsignment>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Commodities of the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink("1");

            ExpectRow(commodityCountry);

            //Completes the Shipment
            Click("Complete");
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Progress").Expect("Ready to Transmit");

            //Transmits the Shipment
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect("Transmit to HRMC");
            System.Threading.Thread.Sleep(1000);
            Click("Transmit to HRMC");

            //Views the ASM Logs
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");

            ExpectHeader("Logs");
            ExpectRow("CreateASMDeclarationRequest");

            //Manually download and view ASM Request - Assert that Request is sent as an IFD
        }
    }
}