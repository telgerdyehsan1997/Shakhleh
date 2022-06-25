using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCommoditiesForTruckersLTDUSDEUR : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentsForTruckersLimited_USD_EUR>();

            LoginAs<ChannelPortsAdmin>();


            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            Click("2");


            AtRow("T072100000101").Click("0");
            Click("New Commodity");

            ExpectHeader("Commodity Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IPAD");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IPAD");
            Set("Gross weight").To("5.25");
            Set("Net weight").To("4.991");
            Set("Value").To("300");
            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GR - Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            Set("Second quantity").To("2");
            Click("Save");
            WaitToSeeHeader(That.Contains, "Commodities");
            AtRow("ABS12343").Column("Currency").Expect("Great Britain - GBP");

            Click("Transmit");

            AtRow("T072100000102").Click("0");
            Click("New Commodity");

            WaitToSeeHeader(That.Contains, "Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IPAD");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IPAD");
            Set("Gross weight").To("5.25");
            Set("Net weight").To("4.991");
            Set("Value").To("500");
            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GR - Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            Set("Second quantity").To("2");

            Click("Save");
            AtRow("ABS12343").Column("Currency").Expect("Great Britain - GBP");

            Click("Transmit");

            AtRow("T072100000101").Expect("Ready to transmit");
            AtRow("T072100000102").Expect("Ready to transmit");
        }
    }
}