using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyInCompanyConsignmentForm : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsShipmentForTruckersLtd, AddCompanyOmega, AddCompanyAlpha>();
            LoginAs<JohnSmithCustomer>();
            AssumeDate("1/1/2019");
            Goto("/");

            // Navigation
            ExpectHeader(That.Contains, "Shipments Into Uk");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitForNewPage();
            ExpectHeader("Consignment Details");
            NearField("Partner name").Click("AddCompany");
            WaitToSeeHeader("Add Company");

            //assert focus is on Company name on load
            Type("TEST-MESSAGE-123");
            ClickField("Town/city");
            AtField("Company name").ExpectValue("TEST-MESSAGE-123");

            // Validate required fields
            Set("Company name").To("");
            Click(The.Top, "Save");
            Expect("The Company name field is required.");
            Expect("The Country field is required.");
            Expect("The Postcode/Zip code field is required.");
            Expect("The Address line 1 field is required.");
            Expect("The Town/city field is required.");
            // Mandatory field selected by default
            ExpectNo("The Representation type field is required.");
            //EORI validation won't occur while Country is empty
            ExpectNo("The EORI number field is required.");

            // Add Company - Omega
            // 4/5 fields match - new company record created
            Set("Company name").To("Omega Ltd");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Postcode").To("WR5 3DA");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB683470514001");
            Set("Payment type").To("Code B");
            Set("Deferment number").To("9876549");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            NearField("Declarant").Click("AddCompany");
            WaitToSeeHeader("Add Company");

            // Add Company - Alpha
            // 4/5 fields match - new company record created
            Set("Company name").To("Alpha Ltd");
            Set("Country").To("");
            ClickHeader("Add Company");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Click("United Kingdom");
            Set("Postcode").To("WR5 3DA");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB123456782012");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("9874543");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            Set("UK Trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 9874543");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Alpha Ltd - Worcester - WR5 3DA - GB123456782012 - 9874543");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.99");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Click("Great Britain - GBP");
            Set("Total value").To("300");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");
            //Set("UCR").To("9GB111222333444-R012000000102");
            ClickButton(That.Contains, "Save");

            ExpectHeader(That.Contains, "Commodities");

            Click(What.Contains, "Shipments Into UK");
            ExpectHeader(That.Contains, "Shipments Into Uk");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Consignments").ClickLink();

            // NOTE: Issue when checking header - skipping for now
            ExpectHeader(That.Contains, "Consignments");
            ExpectRow("R011900000101");
            AtRow("R011900000101").Column("UK Trader").Expect("Alpha Ltd");
            AtRow("R011900000101").Column("Partner").Expect("TRUCKERS LTD");
            AtRow("R011900000101").Column("Declarant").Expect("Channel Ports");

            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            Below(The.Top, "Omega Ltd").ExpectRow("Omega Ltd");
            Below(The.Top, "Alpha Ltd").ExpectRow("Alpha Ltd");
            AtRow(The.Bottom, "Omega Ltd").Column("Type").Expect("Other");
            AtRow(The.Bottom, "Alpha Ltd").Column("Type").Expect("Other");
            //check 'Direct' representation type was selected by default
            AtRow(That.Contains, "9876549").Column("Company name").ClickLink();
            WaitToSeeHeader("Omega Ltd");
            Near("Representation type").Expect("Direct");
            Click("Companies");
            AtRow(That.Contains, "9874543").Column("Company name").ClickLink();
            WaitToSeeHeader("Alpha Ltd");
            Near("Representation type").Expect("Direct");
        }
    }
}
