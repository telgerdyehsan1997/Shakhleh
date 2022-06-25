using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCommodityToSFDConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<AddSFDConsignmentToShipment>();
            LoginAs<ChannelPortsAdmin>();

            //Find shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Commodity page of the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink();

            //Adds the Commodity to the SFD Consignment
            this.AddSFDCommodity(
                countryOfOrigin: "GR - Greece",
                descriptionOfGoods: "SFD Goods");

            //Completes the SFD Shipment
            ClickButton("Complete");
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Progress").Expect("Ready to Transmit");
        }
    }
}