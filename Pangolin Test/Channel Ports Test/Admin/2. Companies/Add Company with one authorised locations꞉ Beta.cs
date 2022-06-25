using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyWithOneAuthorisedLocationsBeta : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCountry_France, AddAuthorisedLocationWarehouse1, PaymentTypeB>();
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

            ClickLabel("Out of UK");
            AtLabel("NCTS").ClickLabel("Sometimes");
            ExpectNo("Customer account number");

            //Set("Postcode/Zip code").To("WR5 3DA");
            ClickLabel("Customer");

            Set("Customer account number").To("A2222");

            Set("Company name").To("Beta Ltd");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            AtLabel("Postcode/Zip code").ClickField();
            Type("WR5 3DA");
            Set("Address Line 1").To("Lock View");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Branch identifier").To("BR001");
            Set("AEO number").To("ACBDEFGHIJ1234567890");
            //Set("TSP").To("ACBDEFGHIJ1234567891");
            //AtLabel("CFSP").ClickLabel("Yes");
            ClickField("Default declarant");
            Type("Shipping C");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("7654321");
            Set("VAT by DAN").To("No");
            Near("Representation type").ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("54321");
            Set("Guarantee type").To("L");
            Set("TIN").To("BR887766554433");
            Set("PIN").To("BR223344556677");


            Set("Authorised locations").To("Warehouse 1");
            Click("Save");

            ExpectRow(That.Contains, "Beta Ltd");
        }
    }
}
