using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyInConsignmentExactMatchCreatesAssociation : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsShipmentForTruckersLtd, AddCompanyOmega, AddCompanyAlpha, AddCompanyDelta>();
            LoginAs<JohnSmithCustomer>();

            // Navigation
            Click("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            //WaitForNewPage();
            //Click("New Consignment");
            WaitToSeeHeader("Consignment Details");

            NearField("Partner name").Click("AddCompany");
            WaitToSeeHeader("Add Company");
            // 5/5 fields match so association created - no new company added
            Set("Company name").To("Omega Ltd");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("WR5 3DA");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB175959155000");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("7654321");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            NearField("Declarant").Click("AddCompany");
            WaitToSeeHeader("Add Company");
            // 5/5 fields match to association created
            Set("Company name").To("Alpha Ltd");
            Set("Country").To("");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("WR5 3DA");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB175959155000");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("7654321");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            NearField("UK Trader").Click("AddCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Delta Ltd");
            Set("Country").To("");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("WR5 3DA");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("Worcester");
            Set("EORI number").To("GB175959155000");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("7654321");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            // complete consignment
            Set("UK Trader").To("");
            ClickField("UK Trader");
            Click(What.Contains, "Delta Ltd");
            ClickField("Partner name");
            Type("Truckers");
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            ClickField("Declarant");
            Click(What.Contains, "Omega Ltd");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.251");
            Set("Total net weight").To("4.9911");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            Set("Invoice currency").To("Great Britain - GBP"); ;
            Set("Total value").To("300");
            Click(What.Contains, "Select");
            Click(What.Contains, "FAS");
            Click(What.Contains, "Save");

            // check no new companies created
            /*
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            Below(The.Top, "Omega Ltd").ExpectNoRow("Omega Ltd");
            Below(The.Top, "Alpha Ltd").ExpectNoRow("Alpha Ltd");
            Below(The.Top, "Delta Ltd").ExpectNoRow("Delta Ltd");*/

            // check that the associations exist and they can be used next time
            LoginAs<JohnSmithCustomer>();
            // Navigation
            Click("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader("Consignment Details");

            Set("UK trader").To("");
            ClickField("UK Trader");
            Type("Ltd");
            Expect(What.Contains, "Alpha");
            Expect(What.Contains, "Omega");
            Expect(What.Contains, "Delta");
            Set("UK trader").To("");

            Press(Keys.Tab);
            Press(Keys.Tab);

            Set("Partner name").To("");
            //ClickField("Partner name");
            //Type("Ltd");
            Expect(What.Contains, "Truckers");
            //Set("Partner name").To("");

            Press(Keys.Tab);
            Press(Keys.Tab);

            Set("Declarant").To("");
            ClickField("Declarant");
            Type("Ltd");
            Expect(What.Contains, "Alpha");
            Expect(What.Contains, "Omega");
            Expect(What.Contains, "Delta");
        }
    }
}
