using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class MajimaConstruction_TransmitShipmentOutOFUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";
            var consignmentNumber = "T072100000101";
            var commodityCountry = "Spain";

            Run<AddCommodityForMajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Commodities of the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink("1");

            ExpectRow(commodityCountry);

            //Completes the Shipment
            Click("Transmit");
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
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");

            ExpectHeader("Logs");
            ExpectRow("CreateASMDeclarationRequest");

            //Manually download and view ASM Request
        }
    }
}