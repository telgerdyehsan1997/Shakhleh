using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitShipmentIntoUK_LicenceIncluded : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodityCountry = "Greece";

            Run<AddCommodity_LicenceIncluded>();
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

            //Manually download and view ASM Request to see if it contains the following at the bottom of the request:
          /*<producedDocument>
             <code xmlns = "asm.org.uk/Sequoia/DeclarationGbProducedDocument">L001</code> - This is the CHIEF Licence Code
            <status xmlns= "asm.org.uk/Sequoia/DeclarationGbProducedDocument" >INTO</status> - This is the status code
            <reference xmlns= "asm.org.uk/Sequoia/DeclarationGbProducedDocument">P1/P2/</reference> - this is the licence number part 1 is the licence identifier and part 2 is the licence number entered on the commodity page
            <quantity xmlns= "asm.org.uk/Sequoia/DeclarationGbProducedDocument">5</quantity> - this is the quantity entered on the commodity page (if required)
          </producedDocument> */
        }
    }
}