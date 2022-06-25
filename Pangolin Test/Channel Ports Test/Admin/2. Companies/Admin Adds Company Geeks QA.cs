using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsCompanyGeeksQA : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France, PaymentTypeB>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------


            ClickLink("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //create new company
            Click("New Company");

            WaitToSeeHeader(That.Contains, "Record Details");
            ClickLabel("Out of UK");
            ClickLabel("Into UK");
            ClickLabel(The.Top, "Sometimes");
            Set("Type").To("Customer");
            Set("Customer account number").To("A1258");
            Set("Company name").To("GeeksQA");
            ClickField("Country");
            Type("France");
            Click(The.Bottom, What.Contains, "FR - France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("Lock View");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Branch identifier").To("BR567");
            Set("AEO number").To("IEAEOC99887766554433");
            //Set("TSP").To("AB556677889911223344");
            AtLabel("CFSP").ClickLabel("None");
            Set("Default declarant").To("Imports");
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("7654321");
            AtLabel("VAT By DAN").ClickLabel("No");
            ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("None");
            Set("TIN").To("BR887766554433");
            Set("PIN").To("BR223344556677");
            Click("Save");
            WaitToSeeHeader("Companies");
        }
    }
}
