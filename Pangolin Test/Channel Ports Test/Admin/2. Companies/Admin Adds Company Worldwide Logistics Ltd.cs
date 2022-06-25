using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsCompanyWorldwideLogisticsLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France, PaymentTypeA>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //create new company
            Click("New Company");
            WaitToSeeHeader(That.Contains, "Record Details");
            ClickLabel("Into UK");
            ClickLabel("Customer");
            Set("Customer account number").To("A1235");
            Set("Company name").To("Worldwide Logistics Ltd");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("Lock Keepers Cottage");
            Set("Address Line 2").To("Basin Road");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Branch identifier").To("BR123");
            Set("AEO number").To("IEAEOC11223344556677");
            //Set("TSP").To("AB123456789123456789");
            AtLabel("CFSP").ClickLabel("None");
            Set("Default declarant").To("Imports Ltd");
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Payment type").To("A - CODE A");
            Set("Deferment number").To("1234567");
            AtLabel("VAT by DAN").ClickLabel("No");
            Near("Representation type").ClickLabel("Direct");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("12345");
            Set("Guarantee type").To("A");
            Set("TIN").To("BR012345678910");
            Set("PIN").To("BR987654321012");
            Click("Save");

            // ----------------------------------------------

            //assert details in list
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Company name").Expect("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Customer account number").Expect("A1235");
            AtRow("Worldwide Logistics Ltd").Column("Address").Expect("Lock Keepers Cottage, Basin Road, Worcester, WR5 3DA");
            AtRow("Worldwide Logistics Ltd").Column("Country").Expect("FR");
            AtRow("Worldwide Logistics Ltd").Column("Type").Expect("Customer");
            AtRow("Worldwide Logistics Ltd").Column("Payment type").Expect("A - Code A");
            AtRow("Worldwide Logistics Ltd").Column("Deferment number").Expect("1234567");
        }
    }
}
