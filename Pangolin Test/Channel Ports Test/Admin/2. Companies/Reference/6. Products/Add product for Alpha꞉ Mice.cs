using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForAlphaMice : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyAlpha>();

            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow(That.Contains, "Alpha Ltd").Click("Alpha Ltd");
            Click("Products");
            Click("New Product");

            Set("Product code").To("MIC12343");
            Set("Description of goods").To("Mice");
            ClickField("Commodity code");
            Type("12345678");
            Click("12345678 - 12");
            Set("Additional code").To("4444");
            Set("Quota").To("666666");

            AtLabel("Licenced").ClickLabel("Yes");
            Set("Export licence").To("3456");
            Click("Save");

            ExpectRow("Mice");
        }
    }
}