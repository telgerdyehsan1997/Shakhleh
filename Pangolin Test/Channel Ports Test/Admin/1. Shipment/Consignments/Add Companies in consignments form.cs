using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompaniesInConsignmentsForm : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewShipmentForTruckersLtd>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // Navigation
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            AtRow(That.Contains, "R0721000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");

            NearField("UK Trader").ClickLink("AddCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Delta Transport Ltd");
            Set("Country").To("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514220");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("9876548");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            NearField("Partner name").ClickLink("AddPartnerCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Omega Transport Ltd");
            Set("Country").To("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Click(The.Top, "Save");

            NearField("Declarant").ClickLink("AddDeclarantCompany");
            WaitToSeeHeader("Add Company");
            Set("Company name").To("Alpha Transport Ltd");
            Set("Country").To("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("W1J 9HS");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");
            Set("Payment type").To("CODE B");
            Set("Deferment number").To("9874543");
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            Click(The.Top, "Save");

            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.95");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            AtField("Invoice currency").Type("GBP");
            Click(What.Contains, "Great Britain - GBP");
            Click(What.Contains, "---Select---");
            Expect(What.Contains, "FAS");
            Click(What.Contains, "FAS");
            Set("Total value").To("1000");
            Set("Total value").To("300");
            Click("Save and Add Commodities");

            Click("Companies");
            ExpectRow("Alpha Transport Ltd");
            ExpectRow("Omega Transport Ltd");
            ExpectRow("Delta Transport Ltd");
        }
    }
}