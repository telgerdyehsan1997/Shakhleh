using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCustomerShipmentCommodities : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            //Goes through workflow which is already covered in the pre-conditions meaning this test is redundant
            /*
            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd, JohnSmithAddsCommodityToConsignment, AdminAddsProduct_IPad>();
            // Navigate    
            LoginAs<JohnSmithCustomer>();
            Set("date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Click("Edit");
            WaitToSeeHeader(That.Contains, "Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");

            // Assert layout
            Expect("New Commodity");

            Expect("Consignment total gross weight");
            Near("Consignment total gross weight").Expect("12 kg");

            Expect("Consignment total net weight");
            Near("Consignment total net weight").Expect("9 kg");

            Expect("Consignment total value");
            Near("Consignment total value").Expect("1,200");


            // Assert new item layout
            Click("New Commodity");
            WaitToSeeHeader(That.Contains, "Details");
            BelowHeader(That.Contains, "Details").ExpectField("Product code");
            BelowField("Product code").ExpectField("Gross weight");
            BelowField("Gross weight").ExpectField("Net weight");
            BelowField("Net weight").ExpectLabel("Currency");
            BelowLabel("Currency").ExpectField("Value");
            BelowField("Value").ExpectButton("Cancel");
            NearButton("Cancel").ExpectButton("Save");

            // Add item - Cancel
            ClickField("Product code");
            Type("IPad");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "IPad - ABS12343");
            Set("Gross weight").To("12");
            Set("Net weight").To("9");
            Set("Value").To("749.99");
            Click("Cancel");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            ExpectRow(That.Contains, "ABS12343");

            // Add item - Save
            Click("New Commodity");
            WaitToSeeHeader(That.Contains, "Details");
            ClickField("Product code");
            Type("IPad");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "IPad - ABS12343");
            Set("Gross weight").To("12");
            Set("Net weight").To("9");
            Set("Value").To("749.99");
            Set("Second quantity").To("22");
            Set("Number of packages for this commodity code (if known)").To("1");
            ClickField("Country of origin");
            Expect(What.Contains, "Spain");
            Click(What.Contains, "Spain");
            Click("Save");

            // Assert new item in list
            AtRow(The.Top, "ABS12343").Column("Product code").Expect("ABS12343");
            AtRow(The.Top, "ABS12343").Column("Gross weight").Expect("12 kg");
            AtRow(The.Top, "ABS12343").Column("Net weight").Expect("9 kg");
            AtRow(The.Top, "ABS12343").Column("Currency").Expect("Great Britain - GBP");
            AtRow(The.Top, "ABS12343").Column("Value").Expect("749.99");
            ExpectText("Total: 1,499.98");

            // Delete - Cancel
            AtRow(The.Top, "ABS12343").Click("Delete");
            Expect("Are you sure you want to delete this commodity?");
            Click("Cancel");
            ExpectRow(The.Top, "ABS12343");

            // Delete - OK
            AtRow(The.Top, "ABS12343").Click("Delete");
            Expect("Are you sure you want to delete this commodity?");
            Click("OK"); */
        }
    }
}
