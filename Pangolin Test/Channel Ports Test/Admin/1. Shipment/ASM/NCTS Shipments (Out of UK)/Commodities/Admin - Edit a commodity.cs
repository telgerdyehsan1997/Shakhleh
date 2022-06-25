using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_EditACommodity : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddA2ndCommodity>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");

            AtRow(The.Bottom, "12GB56789012345678").Column("Commodities").ClickLink();

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            AtRow("12121212 - 14").Column("Edit").ClickLink();

            Set("Description of goods").To("");
            Set("Gross weight").To("");
            Set("Net weight").To("");
            Set("Value").To("");
            Set("Number of packages for this commodity code (if known)").To("");

            Click("Save");

            Expect(What.Contains, "The Description of goods field is required.");
            Expect(What.Contains, "The Gross weight field is required.");
            Expect(What.Contains, "The Net weight field is required.");
            Expect(What.Contains, "The Value field is required.");
            ExpectNo(What.Contains, "The Number of packages field is required.");

            Set("Description of goods").To("This is edited commodity 2");
            Set("Gross weight").To("20");
            Set("Net weight").To("13");
            Set("Value").To("30");

            Click("Save");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            AtRow("12121212 - 14").Column("Gross weight").Expect("20 kg");
            AtRow("12121212 - 14").Column("Net weight").Expect("13 kg");
            AtRow("12121212 - 14").Column("Value").Expect("30");
            AtRow("12121212 - 14").Column("Number of packages").Expect("0");
        }
    }
}