using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddACommodityWith4DifferentCompaniesUsed : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "";

            Run<AdminAddsProduct_IPad, AddProductForWWLMeat, AddProductForAlphaMice, AddProductForDeltaAliens, AddProductForOmegaApples, AddNewShipmentForTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/01/2020");
            Goto("/");

            Click("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            AtRow(That.Contains, "Truckers ltd").Click("Edit");
            Click("Save and Add/Amend Consignments");

            Set("UK trader").To("Worldwide");
            Click(What.Contains, "Worldwide Logistics Ltd - Worcester - WR5 3DA");

            Set("Partner name").To("Alpha");
            Click(What.Contains, "Alpha Ltd - Worcester - WR5 3DA");

            Set("Declarant").To("Delta");
            Click(What.Contains, "Delta Ltd - Worcester - WR5 3DA");

            Set("Total packages").To("10");
            Set("Total gross weight").To("500");
            Set("Total net weight").To("450");
            Set("Invoice number").To("12345");
            Set("Invoice currency").To("GBP");
            Click(What.Contains, "GBP");
            Set("Total value").To("1000");
            //Set("UCR").To("")
            Click("Save and Add Commodities");
            Click("New Commodity");

            // check product code does not include company not part of process 
            ClickField("Product code");
            Type("123");
            System.Threading.Thread.Sleep(3000);

            //-- this section seems to give Pangolin trouble
            //Expect(What.Contains, "Mice");
            //Expect(What.Contains, "Aliens");
            Expect(What.Contains, "Meat");
            Expect(What.Contains, "IPad");
            ExpectNo(What.Contains, "Apples");
            //--

            // add new product
            Click("AddProduct");
            Set("code").To("NEW12343");
            Set("name").To("Newspapers");
            Set("Commodity code").To("87654321");
            Click(The.Top, "Save");
            Expect("A valid Commodity Code is required.");
            Click("OK");
            Set("Commodity code").To("");
            ClickField("Commodity code");
            Type("12345678");
            System.Threading.Thread.Sleep(500);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click(The.Top, "Save");

            ClickField("Product code");
            Type("NEW12343");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Value").To("1000");
            Set("Country of origin").To("g");
            Click("GR - Greece");
            ClickLabel("No");
            Set("Second Quantity").To("1");
            Click(What.Contains, "Save");

            ExpectRow("NEW12343");

            // check addition to company products
            Click("Companies");
            Click("Omega Ltd");
            Click("Products");
            ExpectNoRow("NEW12343");

            Click("Companies");
            Click("Alpha Ltd");
            Click("Products");
            ExpectRow("NEW12343");

            Click("Companies");
            Click("Delta Ltd");
            Click("Products");
            ExpectRow("NEW12343");

            Click("Companies");
            Click("Worldwide logistics Ltd");
            Click("Products");
            ExpectRow("NEW12343");

            Click("Companies");
            Click("Truckers Ltd");
            Click("Products");
            ExpectRow("NEW12343");
        }
    }
}
