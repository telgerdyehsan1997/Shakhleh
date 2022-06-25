using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductWithoutExistingCommodityCodes : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");
            WaitToSeeHeader("Products");
            Click("New Product");
            WaitToSeeHeader("Product Details");

            // ----------------------------------------------

            // Create new product
            Set("Product code").To("ABS12345");
            Set("Product name/Description of goods").To("iPhone");
            Set("Commodity code").To("1234567");
            Set("Additional code").To("5555");
            Set("Quota").To("777777");
            //Set("VAT").To("E");
            AtLabel("Licenced").ClickLabel("Yes");
            Set("Export licence").To("ABC13");
            Click("Save");

            Expect("The Commodity code field is required.");
            Click("OK");

            Set("Commodity code").To("12345678 - 12");
            Click("Save");
            ExpectRow(That.Contains, "ABS12345");

        }
    }
}