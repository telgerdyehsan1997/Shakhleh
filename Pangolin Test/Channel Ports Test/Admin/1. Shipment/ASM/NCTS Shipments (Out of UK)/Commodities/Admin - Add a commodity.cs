using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddACommodity : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddAConsignmentToAnNCTSShipment>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Consignments").ClickLink();

            AtRow(That.Contains, "CP100000001").Column("Commodities").ClickLink();

            //ExpectHeader(That.Contains, "CP100000001 - Commodities");

            Click("New Commodity");

            ExpectHeader("Commodity Details");

            Set("Commodity code").To("1234567");
            Click(What.Contains, "12345678 - 12");

            Set("Description of goods").To("This is commodity 1");
            Set("Gross weight").To("20");
            Set("Net weight").To("7");
            AtLabel("Currency").Expect("EUR");

            Set("Value").To("20");
            Set("Number of packages for this commodity code (if known)").To("");

            Click("Save");

            AtRow("12345678 - 12").Column("Description of goods").Expect("This is commodity 1");
            AtRow("12345678 - 12").Column("Gross weight").Expect("20 kg");
            AtRow("12345678 - 12").Column("Net weight").Expect("7 kg");
            AtRow("12345678 - 12").Column("Currency").Expect("EUR");
            AtRow("12345678 - 12").Column("Value").Expect("20");
            AtRow("12345678 - 12").Column("Number of packages").Expect("0");

            //this comment is just added so I can upload it to Sourcetree as preconditions for this test were fixed
        }
    }
}