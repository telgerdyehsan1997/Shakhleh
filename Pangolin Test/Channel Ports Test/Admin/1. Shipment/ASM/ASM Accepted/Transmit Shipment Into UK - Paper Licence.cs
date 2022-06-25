using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitShipmentIntoUK_PaperLicence : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodityCountry = "Greece";

            Run<AddCommodity_PaperLicence_IntoUK>();
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
            AtRow(consignmentNumber).Column("Progress").Expect("Manual - License");

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
            System.Threading.Thread.Sleep(1000);
            AtRow(consignmentNumber).Column("Actions").Click("Select action");

            //Views the ASM Logs
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");

            ExpectHeader("Logs");
            ExpectRow("CreateASMDeclarationRequest");
            AtRow("CreateASMDeclarationRequest").Column("File").Click("Download");
            ExpectHeader("Logs");

            //Manually download and view ASM Request to see if it doesn't contains the RPTID snipper as it is using a Paper Licnece:
            /*
            <code xmlns="asm.org.uk/Sequoia/DeclarationGbAiStatement">RPTID</code> This is only sent if electronic licence
            <text xmlns="asm.org.uk/Sequoia/DeclarationGbAiStatement">123456</text> this is only sent if electronic licence
            */
        }
    }
}