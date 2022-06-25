using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentWith3AddedCompanies : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France, JohnSmithAddsShipmentForTruckersLtd>();
            LoginAs<JohnSmithCustomer>();

            // Navigation
            Click("Shipments Into UK");
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitForNewPage();

            //Click("New Consignment");
            WaitToSeeHeader("Consignment Details");

            //clear fields
            Set("UK trader").To("");
            Set("Partner name").To("");
            Set("Declarant").To("");

            // add company for uk trader
            NearField("UK trader").Click("AddCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Added UK Trader");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Click(The.Right, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CODE B");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CODE B");
            Set("Deferment number").To("9876549");
            Click(The.Right, "Save");

            // add company for partner 
            NearField("Partner name").Click("AddPartnerCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Added Partner");
            ClickField("Country");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("Paris");
            Set("EORI number").To("GB683470514001");
            //Set("Payment type").To("CODE B");
            //Set("Deferment number").To("9876541");
            Click(The.Top, "Save");

            //  add company for declarant
            NearField("Declarant").Click("AddDeclarantCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Added Declarant");
            ClickField("Country");
            Type("United Kingdom");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "United Kingdom");
            Set("Postcode").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("9876542");
            Click(The.Top, "Save");

            System.Threading.Thread.Sleep(1000);
            Set("Total packages").To("3");
            System.Threading.Thread.Sleep(1000);
            Set("Total gross weight").To("5.2");
            Set("Total net weight").To("4.9");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice currency");
            Type("Great Britain - GBP");
            System.Threading.Thread.Sleep(2000);
            Click("Great Britain - GBP");
            Set("Total value").To("300");
            //ClickField("Country of origin");
            //Type("United Kingdom");
            //System.Threading.Thread.Sleep(2000);
            //Click("United Kingdom");
            //ClickLabel("No");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FAS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FAS");

            Click("Save and Add Commodities");
            //ExpectHeader(That.Contains, "R011900000101 - Commodities");
            //ExpectRow("R011900000101");
            //AtRow("R011900000101").Column("UK Trader").Expect("Added UK Trader");
            //AtRow("R011900000101").Column("Partner").Expect("Added Partner");
            //AtRow("R011900000101").Column("Declarant").Expect("Added Declarant");
        }
    }
}
