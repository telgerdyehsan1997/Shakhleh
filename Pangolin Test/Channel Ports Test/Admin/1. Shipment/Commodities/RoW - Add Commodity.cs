using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class RoW_AddCommodity : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodity = new Constants.CommodityFactory().AddRowCommodity();

            Run<RoW_NewConsignment, NewDeposit_MajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigate to Commodity Page for Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink("0");

            ExpectLink("New Commodity");
            ClickLink("New Commodity");

            ExpectHeader("Commodity Details");

            //Adds the Commodity
            ClickHeader("Commodity Details");
            this.ClickAndWait("Product Code", commodity.Product);
            Set("Gross weight").To(commodity.GrossWeight);
            Set("Net weight").To(commodity.NetWeight);
            Set("Value").To(commodity.Value);
            Set("Number of packages for this commodity code (if known)").To(commodity.NumberOfPackages);
            this.ClickAndWait("Country of origin", commodity.Country);
            ClickButton("Save");

            //Asserts that Commodity has been saved
            ExpectRow("GBP");
        }
    }
}