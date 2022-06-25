using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewConsignmentToNCTSShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOutOfUK>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow("1000000").Column("Consignments").ClickLink("0");

            ExpectLink("NEw Consignment");
            Click("New Consignment");

            ExpectHeader(That.Contains, "Consignment Details");

            Set("EAD MRN").To("12GB34645646455874");
            Click("Search");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");


            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect("United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click("United Kingdom");

            Set("Total packages").To("10");
            Set("Total gross weight").To("145");
            Set("Total net weight").To("60");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect("EUR");
            System.Threading.Thread.Sleep(1000);
            Click("EUR");
            Set("Total value").To("2000");

            Click("Save and Add Commodities");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            Click("New Commodity");

            ExpectHeader("Commodity Details");
            ClickHeader("Commodity Details");
            ClickField("Commodity Code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212 - 14");
            Set("Description of goods").To("Heavy goods");
            Set("Gross weight").To("145");
            Set("Net weight").To("60");
            AtLabel("Currency").Expect("EUR");
            Set("Value").To("2000");
            Set(That.Contains, "Number of packages for this commodity code").To("10");
            Click("Save");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            Click("Complete");

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("UK Trader").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Partner").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Guarantor").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Country of destination").Expect("GB");
            AtRow("CP100000001").Column("Total packages").Expect("10");
            AtRow("CP100000001").Column("Total gross weight").Expect("145 kg");
            AtRow("CP100000001").Column("Total net weight").Expect("60 kg");
            AtRow("CP100000001").Column("Invoice currency").Expect("EUR");
            AtRow("CP100000001").Column("Total value").Expect("2,000.00");
            AtRow("CP100000001").Column("Commodities").Expect("1");
        }
    }
}