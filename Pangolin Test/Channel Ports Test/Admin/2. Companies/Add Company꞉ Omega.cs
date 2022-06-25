using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyOmega : UITest
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
            ClickLabel("Other");
            ExpectNo("Customer account number");
            Set("Company name").To("Omega LTD");
            ClickLabel("Out of UK");
            ClickLabel("Not NCTS");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("Lock View");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Branch identifier").To("BR001");
            Set("AEO number").To("ACBDEFGHIJ1234567891");
            //Set("TSP").To("ACBDEFGHIJ1234567894");
            //AtLabel("CFSP").ClickLabel("No");
            Set("Default declarant").To("Imports");
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("7654321");
            AtLabel("VAT by DAN").ClickLabel("No");
            ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("54321");
            Set("Guarantee type").To("L");
            Set("TIN").To("BR887766554433");
            Set("PIN").To("BR223344556677");
            Click("Save");

            ExpectRow("OMEGA LTD");
        }
    }
}
