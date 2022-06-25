using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCustomerConsignments : UITest
    {
        [Ignore]
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd>();

            LoginAs<JohnSmithCustomer>();

            AssumeDate("1/2/2019");
            Goto("/");

            WaitToSeeHeader(That.Contains, "Shipments");

            AtRow(That.Contains, "6514").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            WaitToSeeHeader(That.Contains, "Consignments");
            Expect("New Consignment");
            AtRow(That.Contains, "R011900000101").Column("Consignment number").Expect("R011900000101");

            AtRow(That.Contains, "R011900000101").Column("Delete").Click("Delete");
            WaitToSee("Are you sure you want to delete this consignment?");
            Click("OK");
            ExpectNoRow(That.Contains, "R011900000101");

            // Assert 'new consignment' layout
            Click("New Consignment");
            WaitToSeeHeader(That.Contains, "Consignment Details");
            Click("Cancel");
            WaitToSeeHeader(That.Contains, "Consignments");
            Click("New Consignment");
            WaitToSeeHeader(That.Contains, "Consignment Details");
            Press(Keys.Tab);
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
            Below("Total value").Expect("Is importer paying the freight"); //TODO: Add question mark           

            // Add new consignment
            Set("UK Trader").To("");
            ClickField("UK Trader");
            Type("Truckers");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Set("Partner name").To("");
            ClickField("Partner name");
            Type("Worldwide");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            Set("Declarant").To("");
            ClickField("Declarant");
            Type("Worldwide");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.991");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice currency");
            Type("GBP");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "GBP");
            Set("Total value").To("300");
            Click(What.Contains, "Save");

            //assert new details
            //AtRow(That.Contains, "R011900000101").Column("Consignment number").Expect("R011900000101");
            //AtRow(That.Contains, "R011900000101").Column("UCR").Expect("9GB987654312000-R011900000101");
            //AtRow(That.Contains, "R011900000101").Column("UK Trader").Expect("Truckers Ltd");
            //AtRow(That.Contains, "R011900000101").Column("Partner").Expect("Worldwide Logistics Ltd");
            //AtRow(That.Contains, "R011900000101").Column("Declarant").Expect("Worldwide Logistics Ltd");
            //AtRow(That.Contains, "R011900000101").Column("Total packages").Expect("3");
            //AtRow(That.Contains, "R011900000101").Column("Total gross weight").Expect("5.25 kg");
            //AtRow(That.Contains, "R011900000101").Column("Total net weight").Expect("4.99 kg");
            //AtRow(That.Contains, "R011900000101").Column("Invoice number").Expect("TRUCKERS-2019-1101");
            //AtRow(That.Contains, "R011900000101").Column("Invoice currency").Expect("GBP");
            //AtRow(That.Contains, "R011900000101").Column("Total value").Expect("300.00");
            //AtRow(That.Contains, "R011900000101").Column("Country of destination").Expect("GB");
            //AtRow(That.Contains, "R011900000101").Column("Preference").Expect("No");
            //AtRow(That.Contains, "R011900000101").Column("Commodities").Expect("0");
        }
    }
}