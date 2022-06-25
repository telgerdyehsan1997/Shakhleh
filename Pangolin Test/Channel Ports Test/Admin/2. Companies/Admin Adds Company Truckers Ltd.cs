using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsCompanyTruckersLtd : UITest
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
            AtLabel("NCTS").ClickLabel("Sometimes");
            // AtLabel("GVMS").ClickLabel("Not GVMS");
            Set("Type").To("Customer");
            Set("Customer account number").To("A1234");
            Set("Company name").To("Truckers Ltd");
            ClickField("Country");
            Type("France");
            Click(The.Bottom, What.Contains, "FR - France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("Lock View");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB347051400262");
            Set("Branch identifier").To("BR567");
            Set("AEO number").To("IEAEOC99887766554433");
            //Set("TSP").To("AB556677889911223344");
            // AtLabel("CFSP").ClickLabel("Yes");
            Set("Default declarant").To("Imports");
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("7654321");
            AtLabel("VAT by DAN").ClickLabel("No");
            ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("54321");
            Set("Guarantee type").To("B");
            Set("TIN").To("BR887766554433");
            Set("PIN").To("BR223344556677");
            Click("Save");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //assert details in list
            ExpectRow("Truckers Ltd");
        }
    }
}
