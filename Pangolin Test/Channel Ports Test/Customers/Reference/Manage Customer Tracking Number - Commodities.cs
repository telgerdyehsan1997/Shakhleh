using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCustomerTrackingNumberCommodities : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsCommodityToConsignment>();

            // Navigate
            LoginAs<JohnSmithCustomer>();
            ExpectHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0119000001");
            AtRow(That.Contains, "R0119000001").Column("Tracking number").Click("R0119000001");
            ExpectHeader("Shipment Details");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            //WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            // Assert layout
            //ExpectHeader("R011900000101 - Commodities");
            ExpectNoButton("Complete");

            Expect("Consignment total gross weight");
            Near("Consignment total gross weight").Expect("12 kg");

            Expect("Consignment total net weight");
            Near("Consignment total net weight").Expect("9 kg");

            Expect("Value");
            Near("Value").Expect("749.99");

            // Assert existing commodity list
            AtRow(That.Contains, "ABS12343").Column("Product code").Expect("ABS12343");
            AtRow(That.Contains, "ABS12343").Column("Gross weight").Expect("12 kg");
            ExpectText("Total: 12 kg");
            AtRow(That.Contains, "ABS12343").Column("Net weight").Expect("9 kg");
            ExpectText("Total: 9 kg");
            AtRow(That.Contains, "ABS12343").Column("Currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "ABS12343").Column("Value").Expect("749.99");
            ExpectText("Total: 749.99");

            // Add new commodity for same consignment
            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("IPad");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "IPad - ABS12343");
            Set("Gross weight").To("24");
            Set("Net weight").To("18");
            Set("Second quantity").To("025");
            Set("Country of origin").To("IT - Italy");
            Set("Value").To("750.01");
            Click("Save");

            // Assert updated commodity details list
            ClickLink("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Tracking number").Click("R0119000001");
            WaitToSeeHeader("Shipment Details");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("2");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            ExpectRow("750.01");
            AtRow(That.Contains, "750.01").Column("Currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "750.01").Column("Net weight").Expect("18 kg");
            AtRow(That.Contains, "750.01").Column("Gross weight").Expect("24 kg");
            AtRow(That.Contains, "750.01").Column("Product code").Expect("ABS12343");
            ExpectText("Total: 1,500");
        }
    }
}