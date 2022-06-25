using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithAddsCommodityToConsignment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd, AdminAddsProduct_IPad>();
            //navigate
            LoginAs<JohnSmithCustomer>();
            Set("Date created").To("28/06/2018");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("0");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            Expect("Consignment total gross weight");
            Near("Consignment total gross weight").Expect("12 kg");

            Expect("Consignment total net weight");
            Near("Consignment total net weight").Expect("9 kg");

            Expect("Consignment total value");
            Near("Consignment total value").Expect("1,200");

            //create new commodity
            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("IPad");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("12");
            Set("Net weight").To("9");
            AtLabel("Currency").Expect("Great Britain - GBP");
            Set("Value").To("749.99");
            ClickField("Country of origin");
            Type("Spain");
            Click("ES - Spain");
            Set("Second quantity").To("1");
            Click("Save");

            //assert new commodity
            AtRow(That.Contains, "ABS12343").Column("Product code").Expect("ABS12343");
            AtRow(That.Contains, "ABS12343").Column("Gross weight").Expect("12 kg");
            AtRow(That.Contains, "ABS12343").Column("Net weight").Expect("9 kg");
            AtRow(That.Contains, "ABS12343").Column("Currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "ABS12343").Column("Value").Expect("749.99");
            AtRow(That.Contains, "ABS12343").Column("Country of origin").Expect("Spain");

            ExpectText("Total: 749.99");
        }
    }
}
