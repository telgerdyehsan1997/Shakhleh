using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddProductForCFSPOwnTestMonitor : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "CFSP OWN TEST";

            Run<AddCompany_CFSPSetToOwn>();
            LoginAs<ChannelPortsAdmin>();

            //Naviagtes to Companies
            this.NavigateToCompanies();

            //Navigates to Products
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Products");
            ExpectHeader("Products");

            //Adds the new Products
            ClickLink("New Product");
            ExpectHeader("Product Details");
            Set("Description of goods").To("Monitors");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12345678 - 12");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12345678 - 12");
            AtLabel("Licenced").ClickLabel("No");
            ClickButton("Save");

            //Asserts that Product has been created
            ExpectHeader("Products");
            ExpectRow(That.Contains, "Monitors");
        }
    }
}