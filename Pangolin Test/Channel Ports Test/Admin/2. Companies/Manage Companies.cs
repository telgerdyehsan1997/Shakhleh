using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCompanies : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, ArchiveCompany_TruckersLtd>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //assert layout
            RightOfHeader("Companies").Expect("New Company");
            Expect("Bulk Upload");
            ExpectLabel("Company name");
            ExpectLabel("Type");
            Expect("Status");

            // ----------------------------------------------

            //enter details and cancel
            ClickLabel("Other");
            ClickLink("New Company");
            ClickLabel("Into uk");
            Set("Company name").To("Global Solutions Ltd");
            Set("Country").To("GB - United Kingdom");
            Set("Postcode/Zip code").To("E2 8JL");
            Set("Address Line 1").To("1 Hackney Road");
            Set("Address Line 2").To("Tower Hamlets");
            Set("Town/city").To("London");
            Set("EORI number").To("GB987654312000");
            Set("Branch identifier").To("BR123");
            Set("AEO number").To("IEAEOC00005827");
            //Set("TSP").To("AB123456789123456789");
            //AtLabel("CFSP").ClickLabel("Yes");
            Set("Default declarant").To("CHANNEL PORTS - HYTHE - CT21 4BL - GB683470514001");
            Click("CHANNEL PORTS - HYTHE - CT21 4BL - GB683470514001");
            Set("Payment type").To("A - CODE A");
            Set("Deferment number").To("1234567");
            AtLabel("VAT By DAN").ClickLabel("Yes");
            ClickLabel("Direct");
            AtLabel("Guarantor Type").ClickLabel("None");
            Set("Transit Guarantee").To("12345");
            Set("TIN").To("BR012345678910");
            Set("PIN").To("BR987654321012");
            Click("Cancel");

            // ----------------------------------------------

            //assert details are not saved in the list
            WaitToSeeHeader("Companies");
            ExpectNoRow("Global Solutions Ltd");

            // ----------------------------------------------

            //create new company, check Mandatory fields behave as expected
            Click("New Company");
            WaitToSeeHeader(That.Contains, "Record Details");
            Click("Save");
            WaitToSee("The Company name field is required.");
            WaitToSee("The Country field is required.");
            WaitToSee("The Postcode/Zip code field is required.");
            WaitToSee("The Address line 1 field is required.");
            WaitToSee("The Town/city field is required.");
            WaitToSee("The Default declarant field is required.");
            Click("Cancel");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            // check for no eori number requirement (non-UK)
            Click("New Company");
            WaitToSeeHeader(That.Contains, "Record Details");
            Set("Company name").To("Worldwide Logistics Ltd");
            ClickLabel("Into uk");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("E2 8JL");
            Set("Address Line 1").To("1 Hackney Road");
            Set("Town/city").To("London");
            ClickField("Default declarant");
            Type("Import");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click("Save");
            ExpectNo(What.Contains, "The EORI number field is required.");
            ExpectHeader(That.Contains, "Companies");

            // ----------------------------------------------

            // check for eori number requirement and validation (UK)
            Click("New Company");
            WaitToSeeHeader(That.Contains, "Record Details");
            ClickLabel("Into uk");
            Set("Company name").To("Global Solutions Ltd");
            ClickField("Country");
            Type("United Kingdom");
            Click("GB - United Kingdom");
            NearLabel("Postcode/Zip code").ClickField();
            Type("E2 8JL");
            Set("Address Line 1").To("1 Hackney Road");
            Set("Address Line 2").To("Tower Hamlets");
            Set("Town/city").To("London");
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            ClickField("Default declarant");
            Type("Import");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click("Save");
            Expect(What.Contains, "The EORI number field is required.");
            Set("EORI number").To("GB987654312000");
            Click("Save");
            Expect(What.Contains, "The EORI number is invalid.");
            Click("OK");
            Set("EORI number").To("GB683470514001");
            Click("Save");
            ExpectNo(What.Contains, "The EORI number is invalid.");
            ExpectHeader(That.Contains, "Companies");
            ExpectRow(That.Contains, "Global Solutions Ltd");



            // ----------------------------------------------

            // Test search with company name - inclusive
            Set("Company name").To("Worldwide Logistics Ltd");
            ClickLabel("All");
            Click("Search");
            WaitToSeeHeader("Companies");
            WaitToSeeRow(That.Contains, "Worldwide Logistics Ltd");
            ExpectNoRow(That.Contains, "Truckers Ltd");

            // ----------------------------------------------

            // Test search with company name - exclusive
            Set("Company name").To("Nothing");
            Click("Search");
            WaitToSeeHeader("Companies");
            ExpectNoRow("Worldwide Logistics Ltd");
            ExpectNoRow("Truckers Ltd");

            // ----------------------------------------------

            // Test search with type - inclusive
            Set("Company name").To("");
            ClickLabel("Customer");
            Click("Search");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            ExpectNoRow("Truckers Ltd");

            // ----------------------------------------------

            // Test search with type - exclusive
            ClickLabel("Customer");
            ClickLabel("Forwarder");
            Click("Search");
            WaitToSeeHeader("Companies");
            ExpectNoRow("Worldwide Logistics Ltd");
            ExpectNoRow("Truckers Ltd");

            // ----------------------------------------------

            // Test search with status - all
            ClickLabel("Forwarder");
            ClickLabel("All");
            Click("Search");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            ExpectRow("Truckers Ltd");

            // ----------------------------------------------

            // Test search with status - Active
            ClickLabel("Active");
            Click("Search");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            ExpectNoRow("Truckers Ltd");

            // ----------------------------------------------

            // Test search with status - Archived
            ClickLabel("Archived");
            Click("Search");
            WaitToSeeHeader("Companies");
            ExpectNoRow("Worldwide Logistics Ltd");
            ExpectRow("Truckers Ltd");

            // ----------------------------------------------

            // check uniqueness rules
            Click("Companies");
            Click("New Company");
            WaitToSeeHeader(That.Contains, "Record Details");
            ClickLabel("Into uk");
            ClickLabel(The.Top, "Customer");
            Set("Customer account number").To("A1234");
            Set("Company name").To("Worldwide Logistics Ltd");
            ClickField("Country");
            Type("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("WR5 3DA");
            Set("Address Line 1").To("1 Hackney Road");
            Set("Address Line 2").To("Tower Hamlets");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Deferment number").To("1234567");
            ClickField("Default declarant");
            Type("Import");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click("Save");

            Expect(What.Contains, "Customer account number must be unique. There is an existing Company record with the provided Customer account number.");
            Click("OK");

            Set("Customer account number").To("A1235");
            Click("Save");

            ExpectNo(What.Contains, "There is an existing Company with the same Branch identifier, EORI number, Name and Postcode/Zip code in the database already.");
            Expect(What.Contains, "There is an existing Company with the same Deferment number, EORI number, Name, Postcode and Town/City in the database already.");
            Click("OK");
            Click("Cancel");

        }
    }
}
