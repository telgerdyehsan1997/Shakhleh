using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Non_AdminAddCompanyDeltaCorporation : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewNon_AdminUser_NormanFreeman, PaymentTypeB>();
            //Run<CreateNewCountry_France>();

            LoginAs<NormanNon_Admin>();
            // ----------------------------------------------

            // Navigation
            //WaitToSeeHeader("Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //create new company
            Click("New Company");

            //create new company
            WaitToSeeHeader(That.Contains, "Record Details");
            ExpectNoLabel("Type");
            ExpectNoLabel("Customer account number");
            ClickLabel("Into UK");
            Set("Company name").To("Delta Corporation");
            ClickField("Country");
            Type("United Kingdom");
            Click("GB - United Kingdom");
            NearLabel("Postcode/Zip code").ClickField();
            Type("WR5 3DA");
            Click("Find Address");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);

            // NOTE: Dependant on implementation/list provided by Postcode search API
            Set("Address Line 1").To("2 Barons Court Road");
            Set("Town/city").To("LONDON");

            Set("EORI number").To("GB123456782012");
            Set("Branch identifier").To("BR123");
            Set("AEO number").To("IEAEOC99887766554433");
            //Set("TSP").To("ACBDEFGHIJ1234767891");
            AtLabel("CFSP").ClickLabel("None");
            ClickField("Default declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Payment type").To("B - CODE B");
            Set("Deferment number").To("1234567");
            AtLabel("VAT by DAN").ClickLabel("No");
            Near("Representation type").ClickLabel("Indirect");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("666");
            Set("Guarantee type").To("54321");
            Set("TIN").To("GB887766554433");
            Set("PIN").To("GB223344556677");
            Click("Save");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //assert details in list
            ExpectRow("Delta Corporation");

            // NOTE: Dependant on implementation/list provided by Postcode search API
            AtRow("Delta Corporation").Column("Address").Expect(What.Contains, "2 BARONS COURT ROAD, LONDON, WR5 3DA");

            AtRow("Delta Corporation").Column("Country").Expect("GB");
            AtRow("Delta Corporation").Column("Type").Expect("Other");
            AtRow("Delta Corporation").Column("Payment type").Expect("B - CODE B");
            AtRow("Delta Corporation").Column("Deferment number").Expect("1234567");
            AtRow("Delta Corporation").ExpectNo("Edit");
            AtRow("Delta Corporation").ExpectNo("Archive");
        }
    }
}