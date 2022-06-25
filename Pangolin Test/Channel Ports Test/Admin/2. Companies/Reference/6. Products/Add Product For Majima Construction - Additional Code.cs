using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForMajimaConstruction_AdditionalCode : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Majima Construction";
            var commodityCode = "12345678 - 12";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();

            //Navigates to Company Products
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Products");
            ExpectHeader("Products");

            //Adds the new Product
            ClickLink("New Product");
            ExpectHeader("Product Details");
            Set("Description of goods").To("Additional Code");
            ClickField("Commodity Code");
            System.Threading.Thread.Sleep(1000);
            Expect(commodityCode);
            System.Threading.Thread.Sleep(1000);
            Click(commodityCode);
            Set("Additional code").To("4567");
            AtLabel("Licenced").ClickLabel("No");
            ClickButton("Save");

            ExpectHeader("Products");
            ExpectRow(That.Contains, commodityCode);
        }
    }
}