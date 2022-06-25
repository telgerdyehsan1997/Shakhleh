using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCustomerShipmentCommodities : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, AdminAddsProductIPhone_Import>();
            //navigate
            LoginAs<JohnSmithCustomer>();

            ExpectHeader("Shipments Into UK");
            Set("Date").To("01/07/2018");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink();
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            Expect("Consignment total gross weight");
            Near("Consignment total gross weight").Expect("12 kg");

            Expect("Consignment total net weight");
            Near("Consignment total net weight").Expect("9 kg");

            Expect("Consignment total value");
            Near("Consignment total value").Expect("1,200");

            //assert edit layout
            AtRow(That.Contains, "ABS12343").Column("Edit").Click("Edit");
            WaitToSeeHeader("Commodity Details");
            BelowHeader("Commodity Details").ExpectField("Product code");
            BelowField("Product code").ExpectField("Gross weight");
            BelowField("Gross weight").ExpectField("Net weight");
            BelowField("Net weight").ExpectLabel("Currency");
            BelowLabel("Currency").ExpectField("Value");
            BelowField("Value").ExpectButton("Cancel");
            NearButton("Cancel").ExpectButton("Save");

            //edit item, cancel
            Set("Gross weight").To("100");
            Set("Net weight").To("99");
            Set("Value").To("1");
            Click("Cancel");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            AtRow(That.Contains, "ABS12343").Column("Gross weight").ExpectNo("100");
            AtRow(That.Contains, "ABS12343").Column("Net weight").ExpectNo("99");
            AtRow(That.Contains, "ABS12343").Column("Value").ExpectNo("1.00");

            //edit item
            AtRow(That.Contains, "ABS12343").Column("Edit").Click("Edit");
            WaitToSeeHeader("Commodity Details");
            Set("Product code").To("");
            ClickField("Product code");
            Type("iPhone");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("40");
            Set("Net weight").To("30");
            AtLabel("Currency").Expect("Great Britain - GBP");
            Set("Value").To("129");
            Click("Save");
            //ExpectText("Please enter a monetary value with up to 2 decimal points");
            //Click("OK");
            //WaitToSeeHeader("R011900000101 - Commodities");
            AtRow("ABS12345").Column("Edit").ClickLink();
            Set("Value").To("");
            Set("Value").To("129");
            ExpectNoText("Please enter a monetary value with up to 2 decimal points");
            ClickLink("Save");

            //assert new details
            // WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            //ExpectNoRow(That.Contains, "ABS12343");
            ExpectRow(That.Contains, "ABS12345");
            AtRow(That.Contains, "ABS12345").Column("Product code").Expect("ABS12345");
            AtRow(That.Contains, "ABS12345").Column("Gross weight").Expect("40 kg");
            AtRow(That.Contains, "ABS12345").Column("Net weight").Expect("30 kg");
            AtRow(That.Contains, "ABS12345").Column("Value").Expect("129");
        }
    }
}
