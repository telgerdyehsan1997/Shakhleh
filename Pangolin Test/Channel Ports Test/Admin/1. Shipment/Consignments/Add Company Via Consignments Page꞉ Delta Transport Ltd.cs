using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyViaConsignmentsPageDeltaTransportLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewShipmentForTruckersLtd>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            WaitToSeeHeader("Shipments");
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            AtRow("TRUCKERS LTD").Column("Consignments").ClickLink();

            Click("New Consignment");

            // ----------------------------------------------

            //create new company
            NearField("UK Trader").ClickLink("AddCompany");
            WaitToSeeHeader("Add Company");

            //assert focus is on Company name on load
            Type("TEST-MESSAGE-123");
            ClickField("Town/city");
            AtField("Company name").ExpectValue("TEST-MESSAGE-123");

            //continue creating company
            Set("Company name").To("Delta Transport Ltd");
            Set("Country").To("France");
            Click(What.Contains, "France");
            Set("Postcode/Zip code").To("SM4 5BE");
            Set("Address Line 1").To("10 Coventry Street");
            Set("Town/city").To("London");
            Set("EORI number").To("GB683470514001");

            //AtLabel("Payment type").Click("---SELECT---");
            //Press(Keys.ArrowDown);
            //Press(Keys.Enter);
            Set("Payment type").To("CODE B");


            Set("Deferment number").To("9876543");
            //do not select any representation type - default should be Direct
            NearField("Representation type").ExpectText(That.Contains, "Direct");
            NearField("Representation type").ExpectText(That.Contains, "Indirect");
            Click(The.Top, "Save");

            // ----------------------------------------------

            //assert details in autocomplete field
            Set("UK Trader").To("");
            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Delta Transport Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Delta Transport Ltd");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Delta Transport Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Delta Transport Ltd");

            ClickHeader("Consignment Details");
            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Delta Transport Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Delta Transport Ltd");

            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.99");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            Set("Invoice currency").To("GBP");
            Click("Great Britain - GBP");
            Set("Total value").To("300");
            AtLabel("Terms of Sale").Click(What.Contains, "---Select---");
            Expect(What.Contains, "FAS");
            Click(What.Contains, "FAS");

            Click("Save and Add Commodities");
            Click("Back");
            Click("Cancel");

            ExpectRow("R072100000101");

            //assert details in list
            Click("Companies");
            ExpectRow("Delta Transport Ltd");
            AtRow("Delta Transport Ltd").Column("Customer account number").ExpectText("");
            AtRow("Delta Transport Ltd").Column("Address").Expect("10 Coventry Street, London, SM4 5BE");
            AtRow("Delta Transport Ltd").Column("Country").Expect("FR");
            AtRow("Delta Transport Ltd").Column("Type").Expect("Other");
            AtRow("Delta Transport Ltd").Column("Payment type").Expect("B - CODE B");
            AtRow("Delta Transport Ltd").Column("Deferment number").Expect("9876543");

            //assert Delta Transport Ltd has Representation Type 'Direct'
            AtRow("Delta Transport Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader(That.Contains, "Delta Transport Ltd");
            Near("Representation type").Expect("Direct");
        }
    }
}