using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForGeeksQA : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyGeeksQA>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow(That.Contains, "GEEKSQA").Click("GEEKSQA");
            Click("Products");
            Click("New Product");

            Set("Product code").To("ALI12343");
            Set("Product name/Description of goods").To("Aliens");
            ClickField("Commodity code");
            Type("12345678");
            Click("12345678 - 12");
            Set("Additional code").To("4444");
            Set("Quota").To("666666");
            //ClickLabel(The.Bottom, That.Equals, "A");
            //Set("Licence override into UK").To("ABC12");

            AtLabel("Licenced").ClickLabel("Yes");
            Set("Export licence").To("3456");
            Click("Save");

            Click("New Product");

            Set("Product code").To("RAF12343");
            Set("Product name/Description of goods").To("Worms");
            ClickField("Commodity code");
            Type("12345678");
            Click("12345678 - 12");
            Set("Additional code").To("5678");
            Set("Quota").To("555555");
            //ClickLabel(The.Bottom, That.Equals, "A");
            //Set("Licence override into UK").To("ABC12");

            AtLabel("Licenced").ClickLabel("Yes");
            Set("Export licence").To("2567");
            Click("Save");

            ExpectRow("Aliens");
            ExpectRow("Worms");

        }
    }
}