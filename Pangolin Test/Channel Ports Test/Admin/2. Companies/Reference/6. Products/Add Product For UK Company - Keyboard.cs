using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForUKCompany_Keyboard : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "UK Company";
            var commodityCode = "12345678 - 12";
            var productCode = "Keyboard";

            Run<AddCompanyUKCompany_DefermentByDeposit>();
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
            Set("Description of goods").To("Keyboard");
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