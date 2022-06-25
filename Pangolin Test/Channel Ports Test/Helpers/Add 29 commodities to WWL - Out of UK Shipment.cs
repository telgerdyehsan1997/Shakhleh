using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Add29CommoditiesToWWLOutofUKShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0222000001";

            Run<AddProductForWWLMeat, AddRouteBlackpoolAndCalais, JennySmithAddsConsignmentForWWL, JennySmithAddsCommoditiesOnAConsigmentForWWL>();
            //navigate
            LoginAs<JennySmithCustomer>();
            AssumeDate("01/01/2020");
            Goto("/");

            Click("Shipments Out of UK");
            WaitToSeeHeader(That.Contains, "Shipments Out of UK");

            this.FindNCTSShipment(trackingNumber);

            AtRow("T0222000001").Column("Consignments").ClickLink();
            ClickLink("New Consignment");

            //add new consignment

            ExpectHeader("Consignment Details");
            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("450");
            Set("Total gross weight").To("29");
            Set("Total net weight").To("29");
            Set("Invoice number").To("WWL125779968");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect("Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click("Great Britain - GBP");

            Set("Total value").To("29");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");
            Click(What.Contains, "Save");

            CreateCommodity();

        }

        private void CreateCommodity()
        {
            for (int i = 0; i <= 28; i++)
            {
                ClickLink("New Commodity");
                ClickField("Product code");
                System.Threading.Thread.Sleep(1000);
                Expect("MEAT - MEA12343");
                System.Threading.Thread.Sleep(1000);
                Click("MEAT - MEA12343");
                Set("Gross weight").To("1");
                Set("Net weight").To("1");
                RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025");
                Set("Second quantity").To("100");
                Set("Value").To("1");
                Set("Number of packages for this commodity code (if known)").To("15");
                ClickField("Country of destination");
                System.Threading.Thread.Sleep(1000);
                Expect("ES - Spain");
                System.Threading.Thread.Sleep(1000);
                Click("ES - Spain");
                Click("Save");
                ExpectHeader(That.Contains, "T022200000102 - Commodities");
            }
        }
    }
}
