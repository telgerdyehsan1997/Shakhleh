using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForMajimaConstructionMultipleVATRates : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Majima Construction";
            var commodityCode = "1012100 - 0";
            var productCode = "Multiple";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2, ImportCommodityCodesWithMultipleVATRates>();
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
            Set("Product code").To(productCode);
            Set("Description of goods").To("Multiple VAT Rates");
            ClickField("Commodity Code");
            System.Threading.Thread.Sleep(1000);
            Expect(commodityCode);
            System.Threading.Thread.Sleep(1000);
            Click(commodityCode);
            Set("VAT").To("S,Z,A");
            AtLabel("Licenced").ClickLabel("No");
            ClickButton("Save");

            ExpectHeader("Products");
            ExpectRow(That.Contains, productCode);
        }
    }
}