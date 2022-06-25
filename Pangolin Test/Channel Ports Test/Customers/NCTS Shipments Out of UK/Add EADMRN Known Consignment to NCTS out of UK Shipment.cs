using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddEADMRNKnownConsignmentToNCTSOutOfUKShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOutOfUK>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");
            Click("New Consignment");

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("ABGBE1234578901231");
            Click("Search");

            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Spain");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Spain");

            Set("Total packages").To("2");
            Set("Total gross weight").To("5");
            Set("Total net weight").To("3");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Total value").To("500.5");

            Click("Save and Add Commodities");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            Click("New Commodity");
            ExpectHeader("Commodity Details");
            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "34545343453");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "34545343453");

            Set("Description of goods").To("iPod 32GB");
            Set("Gross weight").To("5");
            Set("Net weight").To("3");
            Set("Value").To("500.5");
            Set("Number of packages for this commodity code (if known)").To("2");
            Click("Save");
            AtRow("34545343453 - 14").Column("Commodity code").Expect("34545343453 - 14");
            AtRow("34545343453 - 14").Column("Description of goods").Expect("iPod 32GB");
            AtRow("34545343453 - 14").Column("Gross weight").Expect("5 kg");
            AtRow("34545343453 - 14").Column("Net weight").Expect("3 kg");
            AtRow("34545343453 - 14").Column("Currency").Expect("Great Britain - GBP");
            AtRow("34545343453 - 14").Column("Value").Expect("500.5");
            AtRow("34545343453 - 14").Column("Number of packages").Expect("2");
            AtRow("34545343453 - 14").Column("Edit").Expect("Edit");

            Click("NCTS Shipments Out of UK");

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");

            ExpectRow("CP100000001");
            AtRow("CP100000001").Column("UK Trader").Expect("TRUCKERS LTD");
            AtRow("CP100000001").Column("Partner").Expect("TRUCKERS LTD");
            AtRow("CP100000001").Column("Guarantor").Expect("TRUCKERS LTD");
            AtRow("CP100000001").Column("Country of destination").Expect("ES");
            AtRow("CP100000001").Column("Total packages").Expect("2");
            AtRow("CP100000001").Column("Total gross weight").Expect("5 kg");
            AtRow("CP100000001").Column("Total net weight").Expect("3 kg");
            AtRow("CP100000001").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow("CP100000001").Column("Total value").Expect("500.50");
            AtRow("CP100000001").Column("Commodities").Expect("1");
            AtRow("CP100000001").Column("Progress").Expect("DraftNormal");
            AtRow("CP100000001").Column("Edit").Expect("Edit");
            AtRow("CP100000001").Column("Delete").Expect("Delete");
        }
    }
}