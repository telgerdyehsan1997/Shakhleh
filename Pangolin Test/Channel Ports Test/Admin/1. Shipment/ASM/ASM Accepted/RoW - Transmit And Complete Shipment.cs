using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class RoW_TransmitAndCompleteShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodityCountry = "JAPAN";

            Run<RoW_AddCommodity>();
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
            ExpectHeader("Duty is Payable on one or more of the commodity codes");
            ClickButton("Yes");

            ExpectHeader("Shipments");

            //Transmits the Shipment;
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "View Logs");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");
            System.Threading.Thread.Sleep(1000);

            //Views the ASM Logs
            ExpectHeader("Logs");
            ExpectRow("CreateASMDeclarationRequest");
        }
    }
}