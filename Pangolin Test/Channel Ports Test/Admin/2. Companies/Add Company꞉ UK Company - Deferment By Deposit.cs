using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyUKCompany_DefermentByDeposit : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<PaymentTypeA>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            ClickLink("Companies");

            ExpectHeader("Companies");
            ClickLink("New Company");

            //Sets the new Company Details
            Set("Type").To("Customer");
            AtLabel("Transaction type(s)").ClickLabel("Into uk");
            AtLabel("GVMS").ClickLabel("Sometimes");
            AtLabel("Safety and security inbound").ClickLabel("Sometimes");
            AtLabel("Safety and security outbound").ClickLabel("Sometimes");
            Set("Customer account number").To("A8778");
            Set("Company name").To("UK Company");
            this.ClickAndWait("Country", "GB - United Kingdom");
            Set(That.Contains, "Postcode").To("TW1 3DY");
            Set("Address line 1").To("Dock & Slipway");
            Set("Address line 2").To("Eel Pie Island");
            Set("Town/City").To("TWICKENHAM");
            Set("EORI number").To("GB683470514002");
            AtLabel("CFSP").ClickLabel("None");
            Set("Payment type").To("A - CODE A");
            Set("Deferment number").To("2345678");
            AtLabel("VAT by DAN").ClickLabel("Yes");
            AtLabel("Guarantor Type").ClickLabel("None");
            Set("TIN").To("GB887766554422");
            Set("PIN").To("GB223344556622");
            ClickButton("Save");

            //Asserts that the Company has been saved
            ExpectHeader("Companies");
            ExpectRow("UK Company");
        }
    }
}