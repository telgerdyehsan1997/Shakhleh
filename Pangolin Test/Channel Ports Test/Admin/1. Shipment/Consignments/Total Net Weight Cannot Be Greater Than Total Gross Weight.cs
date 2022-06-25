using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TotalNetWeightCannotBeGreaterThanTotalGrossWeight : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentForWWL>();

            LoginAs<ChannelPortsAdmin>();
            AssumeDate("02/01/2020");
            Goto("/");

            WaitToSeeHeader("Shipments");
            ClickLabel(The.Top, "All");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "T0721000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectRow("T072100000101");
            AtRow("T072100000101").Column("Edit").Click("Edit");

            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");
            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("1");
            Set("Total gross weight").To("1.1");
            Set("Total net weight").To("1.2");
            Set("Invoice number").To("Weight test");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect("Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click("Great Britain - GBP");
            Set("Total value").To("1");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");

            Click(What.Contains, "Save");
            Expect(What.Contains, "Total Net Weight cannot be greater than Total Gross Weight.");
            Click("OK");

            Set("Total gross weight").To("1.1");
            Set("Total net weight").To("1.1");
            Click(What.Contains, "Save");
            ExpectNo("Total Net Weight cannot be greater than Total Gross Weight.");

            Click("Back");
            WaitToSeeHeader("Consignment Details");
            Set("Total gross weight").To("1.2");
            Set("Total net weight").To("1.1");
            Click(What.Contains, "Save");
            ExpectNo("Total Net Weight cannot be greater than Total Gross Weight.");
        }
    }
}