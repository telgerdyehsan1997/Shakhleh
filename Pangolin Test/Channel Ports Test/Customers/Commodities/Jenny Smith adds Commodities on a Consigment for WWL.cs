using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsCommoditiesOnAConsigmentForWWL : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddProductForWWLMeat, JennySmithAddsConsignmentForWWL>();
            LoginAs<JennySmithCustomer>();

            Click("Shipments Out of UK");
            WaitToSeeHeader(That.Contains, "Shipments Out of UK");

            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            Click("Search");

            AtRow("T0222000001").Column("Consignments").ClickLink();

            WaitToSeeHeader(That.Contains, "Consignments - Out of UK");
            AtRow("T022200000101").Column("Commodities").ClickLink();

            ExpectNoButton("Transmit");
            ExpectNoButton("Complete");

            Click("New Commodity");

            WaitToSeeHeader(That.Contains, "Commodity Details");
            ClickLabel("Product code");
            Type("Me");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "Meat");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025");
            Set("Second quantity").To("100");
            Set("Value").To("1");
            ClickLabel("Country of destination");
            Type("Spain");
            Click(What.Contains, "ES - Spain");
            Click("Save");

            ExpectButton("Transmit");
            ExpectNoButton("Complete");


        }
    }
}
