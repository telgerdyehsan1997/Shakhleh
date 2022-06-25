using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TotalPackagesMustNotBe0WhenCustomerAddsConsignment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsShipmentForWWL_OutOfUK>();

            LoginAs<JennySmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Click("Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("T0222000001").Click("Edit");
            Click("Save and Add/Amend Consignments");

            // add consignment
            AtLabel("Consignment number").Expect("T022200000101");
            Set("Partner name").To("Worldwide Logistics Ltd - London - E2 8JL - GB987654312000");
            Set("Declarant").To("Worldwide Logistics Ltd - London - E2 8JL - GB987654312000");
            Set("Total packages").To("0");
            Set("Total gross weight").To("100.123");
            Set("Total net weight").To("89.1234");
            Set("Invoice number").To("01234");
            Set("Invoice currency").To("GBP");
            Set("Total value").To("500");
            AtLabel("Terms of Sale").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FAS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FAS");
            Click(What.Contains, "Save");

            Expect(What.Contains, "Total packages should be 1 or more.");
        }
    }
}