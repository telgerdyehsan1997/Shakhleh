using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCommodity : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCommodityToAConsignment>();
            LoginAs<ChannelPortsAdmin>();

            // Navigation
            ExpectHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0721000001");
            AtRow("R0721000001").Column("Tracking number").Click("R0721000001");
            AtRow("R072100000101").Column("Transmit").ClickButton("Transmit");

            ClickLink("Shipments");
            ExpectHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            ;
            AtRow("R0719000001").Column("Edit").Click("Edit");

            //
            Click(What.Contains, "Save");

            AtRow(That.Contains, "R071900000101").Column("Commodities").ClickLink();

            ExpectRow("ABS12343");

            Expect("Consignment total gross weight");
            Near("Consignment total gross weight").Expect("100 kg");

            Expect("Consignment total net weight");
            Near("Consignment total net weight").Expect("90 kg");

            Near("Consignment total packages").Expect("10");

            // Edit - cancel
            AtRow("ABS12343").Column("Edit").Click("Edit");
            Set("Gross weight").To("5");
            Set("Net weight").To("40000");
            Click("Cancel");
            ExpectRow("ABS12343");
            AtRow("ABS12343").Column("Gross weight").Expect("100 kg");
            AtRow("ABS12343").Column("Net weight").Expect("90 kg");
            AtRow("ABS12343").Column("Currency").Expect("Great Britain - GBP");
            AtRow("ABS12343").Column("Value").Expect("1,000");

            // Edit - save
            AtRow("ABS12343").Column("Edit").Click("Edit");
            Set("Gross weight").To("5");
            Set("Net weight").To("40000");
            Click(What.Contains, "Save");
            ExpectRow("ABS12343");
            AtRow("ABS12343").Column("Gross weight").Expect("5 kg");
            AtRow("ABS12343").Column("Net weight").Expect("40,000 kg");
            AtRow("ABS12343").Column("Currency").Expect("Great Britain - GBP");
            AtRow("ABS12343").Column("Value").Expect("1,000");
            Expect("Total: 1,000");

            // Delete - cancel
            AtRow(That.Contains, "ABS12343").Column("Delete").Click("Delete");
            WaitToSeeText("Are you sure you want to delete this commodity?");
            Click("Cancel");
            ExpectRow(That.Contains, "ABS12343");

            // Delete - confirm
            AtRow(That.Contains, "ABS12343").Column("Delete").Click("Delete");
            WaitToSeeText("Are you sure you want to delete this commodity?");
            Click("OK");
            ExpectNoRow(That.Contains, "ABS12343");
        }
    }
}