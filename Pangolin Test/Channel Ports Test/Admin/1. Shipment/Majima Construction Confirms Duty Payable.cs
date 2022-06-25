using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class MajimaConstructionConfirmsDutyPayable : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<AddCommodityForMajimaConstruction_IntoUK_PreferenceNo>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigate to Commodity Page for Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink("1");

            //Completes the Shipment and confirms that Duty can be paid
            ClickButton("Complete");
            ExpectHeader("Duty is Payable on one or more of the commodity codes");
            ClickButton("Yes");
            ExpectHeader("Shipment Details");
        }
    }
}