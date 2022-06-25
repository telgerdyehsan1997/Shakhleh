using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AttemptToAdd100CommoditiesToWWL : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {




            Run<AddProductForWWLMeat, JennySmithAddsConsignmentForWWL, JennySmithAddsCommoditiesOnAConsigmentForWWL>();
            //navigate
            LoginAs<JennySmithCustomer>();
            AssumeDate("01/01/2020");
            Goto("/");

            Click("Shipments Out of UK");
            WaitToSeeHeader(That.Contains, "Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("T0220000001").Click("Edit");
            Click("Save and Add/Amend Consignments");

            WaitToSeeHeader(That.Contains, "Consignments - Out of UK");
            Click("New Consignment");

            //add new consignment

            WaitToSee("Consignment Details");
            Click(The.Bottom, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            ClickLabel("Partner name");
            Click(The.Bottom, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            ClickLabel("Declarant");
            Click(The.Bottom, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Total packages").To("3");
            Set("Total gross weight").To("10.26");
            Set("Total net weight").To("3.89");
            Set("Invoice number").To("WWL125779968");
            Set("Invoice currency").To("EUR");
            Set("Total value").To("5869");
            Click(What.Contains, "Save");

            CreateCommodity();

            Click("New Commodity");
            ClickLabel("Product code");
            Type("Me");
            Click(What.Contains, "Meat");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025");
            Set("Second quantity").To("100");
            Set("Value").To("1");
            Set("Number of packages for this commodity code (if known)").To("15");
            ClickLabel("Country of destination");
            Type("Unit");
            Click(The.Bottom, "GB - United Kingdom");
            Click("Save");
            Expect("A consignment may only have up to 99 commodities. Please remove an existing commodity before adding a new one");
        }

        private void CreateCommodity()
        {
            for (int i = 0; i <= 98; i++)
            {
                Click("New Commodity");
                ClickLabel("Product code");
                Type("Me");
                Click(What.Contains, "Meat");
                Set("Gross weight").To("1");
                Set("Net weight").To("1");
                RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025");
                Set("Second quantity").To("100");
                Set("Value").To("1");
                Set("Number of packages for this commodity code (if known)").To("15");
                ClickLabel("Country of destination");
                Type("Unit");
                Click(The.Bottom, "GB - United Kingdom");
                Click("Save");
                ExpectHeader(That.Contains, "T022000000102 - Commodities");
            }
        }
    }
}
