using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateGrossWeightMismatch : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyAlpha, AddCompanyDelta, AddConsignmentToTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0721000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            Click("New Consignment");

            //add new con
            WaitToSee("Consignment Details");
            AtLabel("Consignment number").Expect("R072100000102");
            Set("UK Trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "DELTA LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654322");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "DELTA LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654322");

            // default declarant working
            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");


            Set("Total packages").To("10");
            Set("Total gross weight").To("17");
            Set("Total net weight").To("10");
            Set("Invoice number").To("TRUCKERS-2019-1101-02");


            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");


            Set("Total value").To("750");

            Set("Terms of Sale").To("EXW - Ex Works");
            Set("Freight currency").To("Great Britain - GBP");
            Set("Freight amount").To("1");
            Click(What.Contains, "Save");
            Click("Back");
            Click("Cancel");

            //assert new details
            AtRow(That.Contains, "R072100000102").Column("Consignment number").Expect("R072100000102");
            AtRow(That.Contains, "R072100000102").Column("UCR").Expect("1GB683470514001-R072100000102");
            AtRow(That.Contains, "R072100000102").Column("UK Trader").Expect("Alpha Ltd");
            AtRow(That.Contains, "R072100000102").Column("Partner").Expect("Delta Ltd");
            AtRow(That.Contains, "R072100000102").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "R072100000102").Column("Total packages").Expect("10");
            AtRow(That.Contains, "R072100000102").Column("Total gross weight").Expect("17 kg");
            AtRow(That.Contains, "R072100000102").Column("Total net weight").Expect("10 kg");
            AtRow(That.Contains, "R072100000102").Column("Invoice number").Expect("TRUCKERS-2019-1101-02");
            AtRow(That.Contains, "R072100000102").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R072100000102").Column("Total value").Expect("750.00");
            AtRow(That.Contains, "R072100000102").Column("Commodities").Click("0");

            Click("New Commodity");
            ExpectHeader("Commodity Details");
            ClickHeader("Commodity Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IPAD - ABS12343");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IPAD - ABS12343");
            AtLabel("Commodity Code").Expect("12345678");
            Set("Gross weight").To("12");
            Set("Net weight").To("10");
            Set("Value").To("500");
            Set("Second quantity").To("1");
            ClickField("Country of origin");
            Expect(What.Contains, "GR - Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");


            Click("Save");


            Click("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            //gross weight smaller
            Column("Weights mismatch").ExpectTick();

            //set gross weight equal

            Click("R0721000001");
            AtRow("R072100000101").Click("Edit");
            Set("Total gross weight").To("12");
            Click("Save and Add Commodities");
            Expect(What.Contains, "Total Net Weight cannot be greater than Total Gross Weight.");
            ClickButton("OK");

            //The following workflow no longer occurs due to updated weight mismatch implementation
            Click("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            //all weights matct
            AtRow("R0721000001").Column("Weights mismatch").ExpectTick();
        }
    }
}
