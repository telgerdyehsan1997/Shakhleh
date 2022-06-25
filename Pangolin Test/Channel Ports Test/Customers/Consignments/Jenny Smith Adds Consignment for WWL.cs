using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsConsignmentForWWL : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsShipmentForWWL_OutOfUK>();

            LoginAs<JennySmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Click("Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("T0222000001").Column("Consignments").ClickLink();
            ClickLink("New Consignment");

            // add consignment
            AtField("UK trader").ExpectValue(That.Contains, "Worldwide");
            Click(The.Bottom, What.Contains, "Worldwide");
            ClickField("Partner name");
            Type("Worldwide");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Worldwide");
            //ClickField("Declarant");
            //Type("Worldwide");
            //System.Threading.Thread.Sleep(1000);
            //Click(What.Contains, "Worldwide");
            Set("Total packages").To("5");
            Set("Total gross weight").To("100.13");
            Set("Total net weight").To("89.123");
            Set("Invoice number").To("01234");
            Type("GBP");
            System.Threading.Thread.Sleep(2000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Total value").To("500");
            // Set("Terms of Sale").To("EXW");
            Click(What.Contains, "---Select---");
            Click(What.Contains, "EXW");
            Click("Save and Add Commodities");

            AtLabel("Consignment total gross weight").Expect(What.Contains, "100.13 kg");
            AtLabel("Consignment total net weight").Expect(What.Contains, "89.12 kg");
            AtLabel("Consignment total value").Expect("500");
            AtLabel("Consignment total packages").Expect("5");

            ExpectNoButton("Transmit");
            ExpectNoButton("Complete");
        }
    }
}