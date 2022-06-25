using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentWithAddedCompaniesAsWWL : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsConsignmentForWWL>();
            LoginAs<JennySmithCustomer>();

            // Navigation
            Click("Shipments Out of UK");
            Set("Date Created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "T0222000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitForNewPage();

            Click("New Consignment");
            WaitToSeeHeader("Consignment Details");

            // add company for uk trader
            NearField("UK Trader").Click("AddCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("WWL UK Trader");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Set("Payment type").To("CODE A");
            Set("Deferment number").To("1876549");
            Click(The.Top, "Save");

            // add company for partner 
            NearField("Partner name").ClickXPath("/html/body/main/div[1]/div/div/form/div[1]/div[3]/div[2]/a/i");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("WWL Partner");
            ClickField("Country");
            Type("FRANCE");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "FRANCE");
            SetXPath("//input[@id='Postcode']").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("FR683470514000");

            Click(The.Top, "Save");

            //  add company for declarant
            NearField("Declarant").ClickXPath("/html/body/main/div[1]/div/div/form/div[1]/div[4]/div[2]/a/i");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("WWL Declarant");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Click(The.Top, "Save");

            Near("UK Trader").Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Near("Partner name").Expect(What.Contains, "WWL PARTNER - LONDON - W1J 9HS - FR683470514000");

            Near("Declarant").Expect(What.Contains, "WWL DECLARANT - LONDON - W1J 9HS - GB683470514001");

            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.99");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice currency");
            Type("GBP");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("300");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");
            Click(What.Contains, "Save");
            Click("Back");
            Click("Cancel");

            //ExpectRow("R011900000101");
            AtRow(That.Contains, "T022200000102").Column("UK Trader").Expect("WWL UK Trader");
            AtRow(That.Contains, "T022200000102").Column("Partner").Expect("WWL Partner");
            AtRow(That.Contains, "T022200000102").Column("Declarant").Expect("Channel Ports");
        }
    }
}