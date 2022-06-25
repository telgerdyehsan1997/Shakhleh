using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignment : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewShipmentForTruckersLtd, AdminAddsCompanyWorldwideLogisticsLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/07/2021");
            Set("Expected date of arrival/departure").To("04/10/2022");
            Set(The.Top, "to").To("01/01/2027");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "RT564744").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");
            ClickLink("Cancel");

            //assert 'new con' layout
            Click("New Consignment");
            WaitToSee("Consignment Details");
            Below("Consignment Details").Expect("Consignment number");
            Below("Consignment number").Expect("UK trader");
            Below("UK trader").Expect("Partner name");
            Below("Partner name").Expect("Declarant");
            Below("Declarant").Expect("Total packages");
            Below("Total packages").Expect("Total gross weight");
            Below("Total gross weight").Expect("Total net weight");
            Below("Total net weight").Expect("Invoice number");
            Below("Invoice number").Expect("Invoice currency");
            Below("Invoice currency").Expect("Total value");

            //add new con
            Set("UK trader").To("Truckers Ltd -");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Truckers Ltd");
            ClickField("Partner name");
            Type("Worldwide Logistics Ltd -");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Worldwide");
            Set("Declarant").To("");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.99");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            Set("Invoice currency").To("GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GBP");
            Set("Total value").To("300");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FAS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FAS");
            Click("Save and Add Commodities");
            Click("Back");
            Click("Cancel");

            //assert new details
            AtRow(That.Contains, "R072100000101").Column("Consignment number").Expect("R072100000101");
            AtRow(That.Contains, "R072100000101").Column("UK Trader").Expect("Truckers Ltd");
            AtRow(That.Contains, "R072100000101").Column("Partner").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "R072100000101").Column("Declarant").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "R072100000101").Column("Total packages").Expect("3");
            AtRow(That.Contains, "R072100000101").Column("Total gross weight").Expect("5.25 kg");
            AtRow(That.Contains, "R072100000101").Column("Total net weight").Expect("4.99 kg");
            AtRow(That.Contains, "R072100000101").Column("Invoice number").Expect("TRUCKERS-2019-1101");
            AtRow(That.Contains, "R072100000101").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R072100000101").Column("Total value").Expect("300.00");
            AtRow(That.Contains, "R072100000101").Column("Commodities").Expect("0");
        }
    }
}
