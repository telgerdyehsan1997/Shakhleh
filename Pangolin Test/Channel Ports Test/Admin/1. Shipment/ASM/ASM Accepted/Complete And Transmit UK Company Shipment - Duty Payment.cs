using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompleteAndTransmitUKCompanyShipment_DutyPayment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodityCountry = "Greece";

            Run<AddCommodityToUKCompanyConsignment, NewDeposit_UKCompany>();
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

            //Confirm that Duty can be paid
            ExpectHeader("Duty is Payable on one or more of the commodity codes");
            ClickButton("Yes");

            //Views the request to see if the correct information is sent
            ExpectHeader("Shipments");
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "View Logs");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Logs");

            ExpectHeader("Logs");
            AtRow("CreateASMDeclarationRequest").Column("File").Click("Download");
            ExpectHeader("Logs");

            //Manually check the request to see if the deferment number is sent
        }
    }
}