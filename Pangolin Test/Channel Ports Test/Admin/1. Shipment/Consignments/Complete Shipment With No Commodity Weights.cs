using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompleteShipmentWithNoCommodityWeights : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<TryToCompleteShipmentWithNonMatchingCommodityValue>();

            LoginAs<ChannelPortsAdmin>();

            Click("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");


            // add 2 empty commodities
            AtRow("T0721000001").ClickLink("1");
            AtRow("T072100000101").Click("1");
            AtRow("1256").Click("Edit");
            Set("Gross weight").To("108.75");
            Set("Net weight").To("56.25 kg");
            Set("Value").To("1500"); //75% of total value
            Set("Number of packages for this commodity code (if known)").To("9");
            Click(What.Contains, "Save");

            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("testProduct1 - 1256");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("36.25");
            Set("Net weight").To("18.75 kg");
            Set("Value").To("500"); //25% of total value
            //AtLabel("Currency").Expect("Great Britain - GBP");
            Set("Value").To("500");
            Set("Number of packages for this commodity code (if known)").To("1");
            ClickField("Country of destination");
            Type("Greece");
            System.Threading.Thread.Sleep(500);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            ClickLabel("No");
            Click(What.Contains, "Save");

            ClickButton("Transmit");

            ClickLink("2");

            AtRow("1,500").Column("Gross weight").Expect("108.75 kg"); //75% of total net weight
            AtRow("1,500").Column("Net weight").Expect("56.25 kg");

            AtRow("500").Column("Gross weight").Expect("36.25 kg");
            AtRow("500").Column("Net weight").Expect("18.75 kg");
        }
    }
}