using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddA2ndCommodity : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<Admin_AddACommodity>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            Set("Date Created").To("28/06/2018");
            Set("Expected date of arrival/departure").To("28/06/2018");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("Commodities").ClickLink("1");

            ExpectHeader(That.Contains, "CP100000001");

            Click("New Commodity");

            ExpectHeader("Commodity Details");

            Set("Commodity code").To("121212");
            Click(What.Contains, "12121212 - 14");

            Set("Description of goods").To("This is commodity 2");
            Set("Gross weight").To("13");
            Set("Net weight").To("6");
            AtLabel("Currency").Expect("EUR");

            Set("Value").To("10");
            Set("Number of packages for this commodity code (if known)").To("7");

            Click("Save");

            AtRow("12121212 - 14").Column("Description of goods").Expect("This is commodity 2");
            AtRow("12121212 - 14").Column("Gross weight").Expect("13 kg");
            AtRow("12121212 - 14").Column("Net weight").Expect("6 kg");
            AtRow("12121212 - 14").Column("Currency").Expect("EUR");
            AtRow("12121212 - 14").Column("Value").Expect("10");
            AtRow("12121212 - 14").Column("Number of packages").Expect("7");
        }
    }
}