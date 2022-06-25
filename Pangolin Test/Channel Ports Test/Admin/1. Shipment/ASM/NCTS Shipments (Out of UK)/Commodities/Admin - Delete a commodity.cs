using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_DeleteACommodity : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddA2ndCommodity>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Column("Edit").ClickLink();

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("Commodities").ClickLink();

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            AtRow("12121212 - 14").Column("Delete").Click("Delete");

            Expect("Are you sure you want to delete this commodity?");
            Click("OK");

            ExpectNo("12121212");
            ExpectNo("This is commodity 2");
            ExpectNo("13");
            ExpectNo("6");
            ExpectNo("EU");
        }
    }
}