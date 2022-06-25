using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithAddsCommodityToConsignmentIPhone : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProductIPhone_Import, JohnSmithAddsConsignmentToShipmentForTruckersLtd>();
            //navigate
            LoginAs<JohnSmithCustomer>();
            AssumeDate("01/01/2019");
            Goto("/");
            WaitToSeeHeader("Shipments Into UK");

            Set("Date created").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Edit").Click("Edit");
            Set("Total gross weight").To("40");
            Set("Total net weight").To("30");
            Set("Total value").To("129.99");
            Click("Save and Add Commodities");

            //create new commodity
            Click("New Commodity");
            ExpectNoButton("Complete");
            ExpectNoButton("Transmit");

            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("iPhone");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("9");
            Set("Net weight").To("8");
            //AtLabel("Currency").Expect("GBP");
            Set("Value").To("1200.00");
            ClickLabel("Country of origin");
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Set("Second quantity").To("1");
            Click("Save");

            //assert new commodity           
            AtRow(That.Contains, "ABS12345").Column("Product code").Expect("ABS12345");
            AtRow(That.Contains, "ABS12345").Column("Gross weight").Expect("9 kg");
            AtRow(That.Contains, "ABS12345").Column("Net weight").Expect("8 kg");
            AtRow(That.Contains, "ABS12345").Column("Value").Expect("1,200");
            ExpectText("Total: 1,200");

            ExpectButton("Complete");
            ExpectNoButton("Transmit");
        }
    }
}
