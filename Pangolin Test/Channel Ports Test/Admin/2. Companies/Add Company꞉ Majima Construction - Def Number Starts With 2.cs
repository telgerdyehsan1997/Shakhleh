using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyMajimaConstruction_DefNumberStartsWith2 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var accountNumber = "A2345";
            var companyName = "Majima Construction";
            var companyCountry = "Japan";
            var postCode = "OSA OB1";
            var addressLine = "Osaka";
            var companyTown = "Sotenbori";
            var defaultDeclarant = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";

            Run<PaymentTypeA, CreateNewCountry_Japan>();
            LoginAs<ChannelPortsAdmin>();
            //Navigates to new Company page
            //this.NavigateToAddCompany();

            //Navigates to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");
            ClickLink("New Company");
            Set("Type").To("Customer");
            AtLabel("Transaction type(s)").ClickLabel("Into uk");
            AtLabel("Transaction type(s)").ClickLabel("Out of uk");
            ExpectLabel("NCTS");
            AtLabel("NCTS").ClickLabel("Sometimes");

            //Sets new labels
            AtLabel("GVMS").ClickLabel("Sometimes");
            AtLabel("Safety and security inbound").ClickLabel("Sometimes");
            AtLabel("Safety and security outbound").ClickLabel("Sometimes");


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
            AtLabel("CFSP").ClickLabel("None");
            ClickField("Default declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, defaultDeclarant);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, defaultDeclarant);
            ClickHeader("Record Details");

            Set("Payment type").To("A - CODE A");
            Set("Deferment number").To("21345678");
            AtLabel("VAT by DAN").ClickLabel("No");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("54321");
            Set("Guarantee type").To("A");
            Set("TIN").To("BR887766554477");
            Set("PIN").To("BR223344556677");
            ClickButton("Save");
            ExpectHeader("Companies");
            ExpectRow(companyName);
        }
    }
}