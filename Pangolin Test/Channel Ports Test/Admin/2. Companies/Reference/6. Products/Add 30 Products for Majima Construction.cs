using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Add30ProductsForMajimaConstruction : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            var companyName = "Majima Construction";
            var commodityCode = "12345678 - 12";
            var productCode = "Meat";

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
            AddProducts();
        }


        private void AddProducts()
        {
            for (int i = 0; i <= 30; i++)
            {
                ClickLink("New Product");
                ExpectHeader("Product Details");
                Set("Product name/Description of goods").To("Meat");
                ClickField("Commodity Code");
                System.Threading.Thread.Sleep(1000);
                Expect("12345678 - 12");
                System.Threading.Thread.Sleep(1000);
                Click("12345678 - 12");
                AtLabel("Licenced").ClickLabel("No");
                ClickButton("Save");

                ExpectHeader("Products");
                ExpectRow("12345678 - 12");
            }
        }
    }
}