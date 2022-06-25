using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForOmegaApples : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyOmega>();

            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow(That.Contains, "Omega").Click("Omega");
            Click("Products");
            Click("New Product");

            Set("Product code").To("APP12343");
            Set("Product name").To("Apples");
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

            ExpectRow("Apples");
        }
    }
}