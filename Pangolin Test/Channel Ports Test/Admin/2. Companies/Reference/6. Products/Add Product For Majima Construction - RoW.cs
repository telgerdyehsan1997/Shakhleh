using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForMajimaConstruction_RoW : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<RoW_ImportCommodityCodes, AddCompanyMajimaConstruction_DefNumberStartsWith2>();
            LoginAs<ChannelPortsAdmin>();

            var companyName = "Majima Construction";
            var commodityCode = "1012100 - 0";

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
            Set("Description of goods").To("RoW Product");
            ClickField("Commodity Code");
            System.Threading.Thread.Sleep(1000);
            Expect(commodityCode);
            System.Threading.Thread.Sleep(1000);
            Click(commodityCode);
            AtLabel("Licenced").ClickLabel("No");
            ClickButton("Save");

            ExpectHeader("Products");
            ExpectRow(That.Contains, commodityCode);
        }
    }
}