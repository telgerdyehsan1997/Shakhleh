using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Add30CommoditiesToWWLOutofUKShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0220000001";

            Run<AddProductForWWLMeat, AdminEditsCompanyWorldwideLogisticsLtdtoNCTS, AddRouteBlackpoolAndCalais, JennySmithAddsCommoditiesOnAConsigmentForWWL>();
            //navigate
            LoginAs<JennySmithCustomer>();
            AssumeDate("01/01/2020");
            Goto("/");

            Click("Shipments Out of UK");
            WaitToSeeHeader(That.Contains, "Shipments Out of UK");

            this.FindShipment(trackingNumber);

            AtRow("T0220000001").Click("Edit");
            AtLabel("NCTS").ClickLabel("No");
            Click("Save and Add/Amend Consignments");

            //add new consignment

            ExpectHeader("Consignment Details");
            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("465");
            Set("Total gross weight").To("31");
            Set("Total net weight").To("31");
            Set("Invoice number").To("WWL125779968");
            Set("Invoice currency").To("EUR");
            Set("Total value").To("31");
            Click(What.Contains, "Save");

            CreateCommodity();



        }

        private void CreateCommodity()
        {
            for (int i = 0; i <= 30; i++)
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
