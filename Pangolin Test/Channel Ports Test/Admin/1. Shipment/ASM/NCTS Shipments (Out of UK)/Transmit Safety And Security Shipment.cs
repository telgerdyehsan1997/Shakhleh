using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitSafetyAndSecurityShipment : UITest
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

            //Navigates to the Commodities of the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            AtRow(consignmentNumber).Column("Commodities").ClickLink();
            ExpectRow("MEAT");

            //Sets the status to 'Ready to Transmit;
            Click("Transmit");
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Progress").Expect("Ready to Transmit");

            //Navigates to Shipment Details
            ClickLink("Shipments");
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Tracking number").ClickLink();
            ExpectRow(consignmentNumber);

            //Transmits the Shipment
            AtRow(consignmentNumber).Column("Actions").ClickButton("Select action");
            Expect("Transmit to HRMC");
            System.Threading.Thread.Sleep(1000);
            Click("Transmit to HRMC");
            RefreshPage();
            System.Threading.Thread.Sleep(1000);

            //Views the ASM of the Shipment
            AtRow(consignmentNumber).Column("Actions").ClickButton("Select action");
            Expect(What.Contains, "View Logs");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");
            ExpectHeader("Logs");
            ExpectRow("CreateASMDeclarationRequest");

            //Manually check the ASM request to see if the Health Certificates are included in the Request
        }
    }
}