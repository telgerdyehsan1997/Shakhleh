using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyPAndLUKLTD : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();
            ClickLink("New Company");
            ExpectHeader("Record Details");

            //Set Company Details
            AtLabel("Type").ClickLabel("Customer");
            AtLabel("Transaction type(s)").ClickLabel("Into uk");
            AtLabel("Transaction type(s)").ClickLabel("Out of uk");
            AtLabel("NCTS").ClickLabel("Sometimes");
            AtLabel("GVMS").ClickLabel("Sometimes");
            AtLabel("Safety and security inbound").ClickLabel("Sometimes");
            AtLabel("Safety and security outbound").ClickLabel("Sometimes");
            Set("Customer account number").To("a9999");
            Set("Company name").To("P&L UK LTD.");
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Expect("GB - United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click("GB - United Kingdom");
            Set(That.Contains, "Postcode").To("LND OA1");
            Set("Address line 1").To("56 London Place");
            Set("Town/City").To("London");
            Set("EORI number").To("GB687770514007");
            AtLabel("CFSP").ClickLabel("None");
            AtLabel("Guarantor Type").ClickLabel("None");
            ClickButton("Save");

            //Assert company has been saved
            ExpectRow("P&L UK LTD.");
        }
    }
}