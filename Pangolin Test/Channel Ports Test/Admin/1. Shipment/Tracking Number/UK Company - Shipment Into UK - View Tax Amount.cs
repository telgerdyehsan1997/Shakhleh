using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UKCompany_ShipmentIntoUK_ViewTaxAmount : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<CompleteAndTransmitUKCompanyShipment_DutyPayment>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "View Tax Amount");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "View Tax Amount");

            //Assert that the correct Tax amount information sent
            ExpectHeader("View Tax Amount");

            //ChannelPorts Deferment number should appear as this is sent in the ASM request
            //ChannelPorts Deferment number is sent as UK Traders deferment number starts with 2
            AtLabel("Deferment Number").Expect("8100765"); 

        }
    }
}