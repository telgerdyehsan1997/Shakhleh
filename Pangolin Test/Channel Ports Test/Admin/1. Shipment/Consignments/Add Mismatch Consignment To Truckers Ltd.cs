using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddMismatchConsignmentToTruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProduct_IPad, AddNewShipmentForTruckersLtd>();
            // navigate
            LoginAs<ChannelPortsAdmin>();

            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            // add new consignment
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);

            ClickField("Partner name");
            Type("World");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "Worldwide");

            // default declarant working
            AtLabel("Declarant").ExpectValue(That.Contains, "Imports Ltd");

            Set("Total packages").To("10");
            Set("Total gross weight").To("100");
            Set("Total net weight").To("99");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice currency");
            Type("GB");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Total value").To("3140");
            Set("Terms of Sale").To("FCA");

            Click("Save and Add Commodities");

            Click("Back");
            WaitToSeeHeader("Consignment Details");
            Click("Cancel");

            AtRow("TRUCKERS-2019-1101").Column("Commodities").ClickLink();

            Click("New commodity");

            ClickField("Product code");
            Type("A");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "IPad");
            Set("Gross weight").To("2");
            Set("Net weight").To("1");
            Set("Value").To("1");
            Set("Second quantity").To("3");

            ClickField("Country of origin");
            Type("Unit");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "GB - United Kingdom");
            Click(What.Contains, "Save");

            Click("Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow("Portsmouth").Column("Weights mismatch").ExpectTick();
        }
    }
}
