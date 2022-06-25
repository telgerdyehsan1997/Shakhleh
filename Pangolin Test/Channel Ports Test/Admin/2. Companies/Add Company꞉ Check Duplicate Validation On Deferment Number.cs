using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyCheckDuplicateValidationOnDefermentNumber : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();
            LoginAs<ChannelPortsAdmin>();

            var accountNumber = "A2346";
            var companyName = "Tachibana Real Estate";
            var companyCountry = "Japan";
            var postCode = "JPN ON1";
            var addressLine = "Tokyo";
            var companyTown = "Kamarucho";
            var defaultDeclarant = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";

            Run<PaymentTypeA, CreateNewCountry_Japan>();

            //Navigates to new Company page
            //this.NavigateToAddCompany();
            ClickLink("Companies");
            ExpectHeader("Companies");
            ClickLink("New Company");
            AtLabel("Type").ClickLabel("Customer");
            AtLabel("Transaction type(s)").ClickLabel("Into uk");
            AtLabel("Transaction type(s)").ClickLabel("Out of uk");
            ExpectLabel("NCTS");
            AtLabel("NCTS").ClickLabel("Always");

            //Sets new labels
            //AtLabel("GVMS").ClickLabel("Always");
            AtLabel("Safety and security inbound").ClickLabel("Always");
            AtLabel("Safety and security outbound").ClickLabel("Always");


            Set("Customer account number").To(accountNumber);
            Set("Company name").To(companyName);
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyCountry);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyCountry);
            Set("Postcode/Zip code").To(postCode);
            Set("Address line 1").To(addressLine);
            Set("Town/City").To(companyTown);
            ClickField("Default declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, defaultDeclarant);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, defaultDeclarant);

            Click(What.Contains, "---Select---");
            Expect(What.Contains, "A - CODE A");
            Click(What.Contains, "A - CODE A");
            Set("Deferment number").To("21345678");
            AtLabel("VAT by DAN").ClickLabel("No");
            ClickButton("Save");
            //Expect(What.Contains, "Deferment number is already taken");
        }
    }
}