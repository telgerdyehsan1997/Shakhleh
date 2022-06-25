using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerCannotCompleteCommoditiesUnlessAllnoWeights : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, AdminAddsProductIPhone_Import>();
            //navigate
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickField("Date created");
            Type("01/01/1900");
            Press(Keys.Tab);
            Type("01/01/2100");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            // WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            //create another new commodity
            Click("New Commodity");
            WaitToSeeHeader(That.Contains, "Commodity Details");
            ClickField("Product code");
            Type("iPhone");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            AtLabel("Currency").Expect(What.Contains, "GBP");
            Set("Value").To("399.99");
            ClickField("Country of origin");
            Expect(What.Contains, "Spain");
            Click(What.Contains, "Spain");
            Click("Save");
            //WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            //try to 'complete' commodities, expect error
            Click("Save");
            Expect(What.Contains, "The Gross weight field is required");
            Expect(What.Contains, "The Net weight field is required.");
            Expect(What.Contains, "The Second quantity field is required.");

            //complete all weights and try to 'complete' again - expect no error;
            Set("Gross weight").To("6");
            Set("Net weight").To("5");
            Set("Second quantity").To("22");
            Click("Save");
            Click("Complete");
            ExpectNo("The gross and net weights for Commodities must either be all completed or all blank.");
        }
    }
}
