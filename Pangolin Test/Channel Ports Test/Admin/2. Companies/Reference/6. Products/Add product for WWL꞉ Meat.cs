using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForWWLMeat : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyWorldwideLogisticsLtd>();

            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow(That.Contains, "Worldwide").Click("Worldwide logistics Ltd");
            Click("Products");
            Click("New Product");

            Set("Product code").To("MEA12343");
            Set("Product name/Description of goods").To("Meat");
            ClickField("Commodity code");
            Type("12345678");
            Click("12345678 - 12");
            Set("Additional code").To("4444");
            Set("Quota").To("666666");
            //ClickLabel(The.Bottom, That.Equals, "A");
            ExpectNo("Export licence");
            AtLabel("Licenced").ClickLabel("Yes");
            Set("Export licence").To("3456");
            Click("Save");

            ExpectRow("Meat");
        }
    }
}