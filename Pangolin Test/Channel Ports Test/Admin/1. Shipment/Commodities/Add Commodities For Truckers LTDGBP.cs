using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCommoditiesForTruckersLTDGBP : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";
            var consignmentNumber = "T072100000101";

            Run<AddConsignmentsForTruckersLimitedGBP>();
            LoginAs<ChannelPortsAdmin>();

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            AtRow(consignmentNumber).Column("Commodities").ClickLink();
            ClickLink("New Commodity");

            ExpectHeader("Commodity Details");
            ClickField("Product code");
            Type("IPad");
            ClickText(That.Contains, "IPad");
            Set("Gross weight").To("5.25");
            Set("Net weight").To("4.991");
            Set("Value").To("300");
            ClickField("Country of destination");
            Type("ES");
            Click("ES - Spain");
            Set("Second quantity").To("1");
            Click("Save");
            WaitToSeeHeader(That.Contains, "Commodities");
            AtRow("ABS12343").Column("Currency").Expect("Great Britain - GBP");
        }
    }
}