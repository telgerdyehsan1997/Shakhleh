using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompanies : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyWorldwideLogisticsLtd, CreateNewCountry_France, PaymentTypeB>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //edit details
            Set("Company name").To("");
            ClickLabel("All");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Companies");
            AtRow("Worldwide Logistics Ltd").Column("Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "Record Details");

            // ----------------------------------------------

            ClickLabel(The.Top, "Forwarder");
            Set("Customer account number").To("A6543");
            Set("Company name").To("Logistics World Ltd");
            AtField("Country").ExpectValue("France");
            Set("Postcode/Zip code").To("N16 7JD");
            Set("Address Line 1").To("100 Stoke Newington High St");
            Set("Address Line 2").To("Islington");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Set("Branch identifier").To("AG321");
            Set("AEO number").To("IEAEOC11223344551111");
            //Set("TSP").To("ACBDEFGHIJ1234567821");
            ClickLabel("CFSP");
            Set("Payment type").To("A - CODE A");
            Set("Deferment number").To("7654321");
            Near("Representation type").ClickLabel("Indirect");
            Set("Transit Guarantee").To("12345");
            Set("TIN").To("BR012345678910");
            Set("PIN").To("BR987654321012");
            Click("Save");

            // ----------------------------------------------

            //assert new details are in list
            WaitToSeeHeader("Companies");
            WaitToSeeRow(That.Contains, "Logistics World Ltd");
            AtRow("Logistics World Ltd").Column("Company name").Expect("Logistics World Ltd");
            AtRow("Logistics World Ltd").Column("Customer account number").Expect("A6543");
            AtRow("Logistics World Ltd").Column("Address").Expect("100 Stoke Newington High St, Islington, London, N16 7JD");
            AtRow("Logistics World Ltd").Column("Country").Expect("FR");
            AtRow("Logistics World Ltd").Column("Type").Expect("Forwarder");
            AtRow("Logistics World Ltd").Column("Payment type").Expect("A - CODE A");
            AtRow("Logistics World Ltd").Column("Deferment number").Expect("7654321");
            ExpectNoRow(That.Contains, "Worldwide Logistics Ltd");

            // ----------------------------------------------

            //check details are saved in Companies > [Company Name]
            AtRow("Logistics World Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader("Logistics World Ltd");
            AtLabel("Customer account number").Expect("A6543");
            AtLabel("Country").Expect("France");
            AtLabel("Postcode/Zip code").Expect("N16 7JD");
            AtLabel("Address line 1").Expect("100 Stoke Newington High St");
            AtLabel("Address line 2").Expect("Islington");
            AtLabel("Town/city").Expect("London");
            AtLabel("Type").Expect("Forwarder");
            AtLabel("EORI number").Expect("GB683470514001");
            AtLabel("Branch identifier").Expect("AG321");
            AtLabel("AEO number").Expect("IEAEOC11223344551111");
            AtLabel("Payment type").Expect("CODE A");
            AtLabel("Deferment number").Expect("7654321");
            AtLabel("Representation type").Expect("Indirect");
            AtLabel("Transit Guarantee").Expect("12345");
            AtLabel("TIN").Expect("BR012345678910");
            AtLabel("PIN").Expect("BR987654321012");
            AtLabel("Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            //Navigate to Edit page for validation checks
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow("Logistics World Ltd").Click("Edit");
            WaitToSeeHeader(That.Contains, "Record Details");

            //Check that account number has validation
            Set("Customer account number").To("ABCDE");
            Click("Save");
            ExpectText("Customer account number must be in the format of \"A1234\".");
            Click("Ok");
            Set("Customer account number").To("A6543");

            //Check that EORI number has validation if Country is GB
            Set("Country").To("");
            ClickHeader("Record Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB - United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB - United Kingdom");
            Set("EORI number").To("");
            Click("Save");
            ExpectText("The EORI number field is required.");
            Set("Country").To("");
            ClickHeader("Record Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FRANCE");
            Click("Save");
        }
    }
}
