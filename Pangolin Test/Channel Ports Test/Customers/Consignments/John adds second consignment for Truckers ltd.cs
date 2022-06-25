using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnAddsSecondConsignmentForTruckersLtd : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd, JennySmithAddsConsignmentForWWL>();
            LoginAs<JohnSmithCustomer>();
            ExpectHeader("Shipments Into UK");

            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");

            AtRow("R0119000001").Column("Consignments").ClickLink();

            // add consignment
            ClickLink("New consignment");
            AtLabel("Consignment number").Expect("R011900000102");

            ClickLabel("Partner name");
            System.Threading.Thread.Sleep(2000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickLabel("Declarant");
            System.Threading.Thread.Sleep(2000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            //Set("Declarant").To("Worldwide Logistics Ltd - London - E2 8JL - GB987654312000");
            Set("Total packages").To("5");
            Set("Total gross weight").To("100.12");
            Set("Total net weight").To("89.12");
            Set("Invoice number").To("01234");
            ClickField("Invoice currency");
            Type("GBP");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "GBP");
            Set("Total value").To("500");

            AtLabel("Terms of Sale").Click(What.Contains, "---Select---");
            Expect(What.Contains, "EXW");
            Click(What.Contains, "EXW");

            ClickField("Freight currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GBP");

            Set("Freight amount").To("1");
            Click("Save and Add Commodities");

            ExpectText("There are no commodities to display.");
        }
    }
}
