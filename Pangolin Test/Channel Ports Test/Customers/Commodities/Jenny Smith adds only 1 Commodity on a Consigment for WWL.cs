using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsonlyoneCommodityOnAConsigmentForWWL : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddProductForWWLMeat, JennySmithAddsConsignmentForWWL, JennySmithAddsCommoditiesOnAConsigmentForWWL>();
            LoginAs<JennySmithCustomer>();

            Click("Shipments Out of UK");
            WaitToSeeHeader(That.Contains, "Shipments Out of UK");

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("T0220000001").Click("Edit");
            Click("Save and Add/Amend Consignments");

            WaitToSeeHeader(That.Contains, "Consignments - Out of UK");
            Click("New Consignment");

            //Set consignment details
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
            ClickLabel("Only 1 Commodity");
            Click("Save and Add Commodities");

            //Set Commodity Details
            WaitToSeeHeader(That.Contains, "Commodity Details");
            ClickLabel("Product code");
            Type("Me");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "Meat");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025");
            Set("Second quantity").To("100");
            Set("Value").To("5869");
            ClickLabel("Country of destination");
            Type("Unit");
            Click(The.Bottom, "GB - United Kingdom");
            Click("Save");

            ExpectButton("Transmit");
            ExpectNoButton("New Commodity");
            ExpectNoButton("Complete");


        }
    }
}
