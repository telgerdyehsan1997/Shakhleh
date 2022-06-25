using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyDelta : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd, CreateNewCountry_France, PaymentTypeB>();
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

            ClickLabel("Out of UK");
            AtLabel("NCTS").ClickLabel("Sometimes");
            ClickLabel("Customer");
            Set("Customer account number").To("D1234");
            Set("Company name").To("Delta Ltd");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("Lock View");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Branch identifier").To("BR001");
            Set("AEO number").To("ACBDEFGHIJ1234567892");
            //Set("TSP").To("ACBDEFGHIJ1234567892");
            Set("Default declarant").To("Truckers");
            Click(What.Contains, "Truckers");
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("7654322");
            AtLabel("VAT by DAN").ClickLabel("No");
            ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("54321");
            Set("Guarantee Type").To("C");
            Set("TIN").To("BR887766554433");
            Set("PIN").To("BR223344556677");


            Click("Save");

            ExpectRow("Delta Ltd");
        }
    }
}
