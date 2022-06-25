using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyAlpha : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France, PaymentTypeB>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            //WaitToSeeHeader("Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //create new company
            Click("New Company");
            WaitToSeeHeader(That.Contains, "Record Details");
            //assert focus is on Company name on load
            Type("TEST-MESSAGE-123");
            ClickField("Town/city");
            AtField("Company name").ExpectValue("TEST-MESSAGE-123");
            Set("Company name").To("Alpha Ltd");
            ClickLabel("Into UK");
            ClickLabel("Customer");
            Set("Customer account number").To("B1234");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("Lock View");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB123456782012");
            Set("Branch identifier").To("BR001");
            Set("AEO number").To("ACBDEFGHIJ1234567890");
            //Set("TSP").To("ACBDEFGHIJ1234567890");
            AtLabel("CFSP").ClickLabel("None");
            Set("Default declarant").To(" Imports Ltd");
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("7654321");
            AtLabel("VAT by DAN").ClickLabel("No");
            Near("Representation type").ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("54321");
            Set("Guarantee type").To("L");
            Set("TIN").To("BR887766554433");
            Set("PIN").To("BR223344556677");
            Click("Save");

            ExpectRow(That.Contains, "Alpha Ltd");
        }
    }
}
