using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageConsignments : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewShipmentForTruckersLtd, AdminAddsCompanyWorldwideLogisticsLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "RT564744").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            //assert 'new con' layout
            ClickLink("Cancel");
            ClickLink("New Consignment");

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
            Below("Total value").Expect("Is importer paying the freight");

            //add new con
            Set("UK trader").To("Truckers Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Truckers Ltd");
            Set("Partner name").To("Worldwide Logistics Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Worldwide Logistics Ltd");
            Set("Declarant").To("Worldwide Logistics Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Worldwide Logistics Ltd");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.99");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            Set("Invoice currency").To("GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GBP");
            Set("Total value").To("300");
            Set("Terms of sale").To("FAS");
            Click(What.Contains, "Save and Add Commodities");

            Click("Back");
            Click("Cancel");

            //assert new details
            AtRow(That.Contains, "R012100000101").Column("Consignment number").Expect("R012100000101");
            AtRow(That.Contains, "R012100000101").Column("UK Trader").Expect("Truckers Ltd");
            AtRow(That.Contains, "R012100000101").Column("Partner").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "R012100000101").Column("Declarant").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "R012100000101").Column("Total packages").Expect("3");
            AtRow(That.Contains, "R012100000101").Column("Total gross weight").Expect("5.25 kg");
            AtRow(That.Contains, "R012100000101").Column("Total net weight").Expect("4.99 kg");
            AtRow(That.Contains, "R012100000101").Column("Invoice number").Expect("TRUCKERS-2019-1101");
            AtRow(That.Contains, "R012100000101").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R012100000101").Column("Total value").Expect("300.00");
            AtRow(That.Contains, "R012100000101").Column("Commodities").Expect("0");

            //add another consignment
            ClickLink("New Consignment");
            WaitToSeeHeader("Consignment Details");
            Set("UK trader").To("Truckers Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Truckers Ltd");
            Set("Partner name").To("Worldwide Logistics Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Worldwide Logistics Ltd");
            Set("Declarant").To("Worldwide Logistics Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Worldwide Logistics Ltd");
            Set("Total packages").To("11");
            Set("Total gross weight").To("11");
            Set("Total net weight").To("9");
            Set("Invoice number").To("TRUCKERS-2019-1102");
            Set("Invoice currency").To("GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GBP");
            Set("Total value").To("450");
            Set("Terms of Sale").To("EXW");

            Click(What.Contains, "Save and Add Commodities");

            Click("Back");
            Click("Cancel");

            //assert new details
            AtRow(That.Contains, "R012100000102").Column("Consignment number").Expect("R012100000102");
            AtRow(That.Contains, "R012100000102").Column("UK Trader").Expect("Truckers Ltd");
            AtRow(That.Contains, "R012100000102").Column("Partner").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "R012100000102").Column("Declarant").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "R012100000102").Column("Total packages").Expect("11");
            AtRow(That.Contains, "R012100000102").Column("Total gross weight").Expect("11 kg");
            AtRow(That.Contains, "R012100000102").Column("Total net weight").Expect("9 kg");
            AtRow(That.Contains, "R012100000102").Column("Invoice number").Expect("TRUCKERS-2019-1102");
            AtRow(That.Contains, "R012100000102").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R012100000102").Column("Total value").Expect("450.00");
            AtRow(That.Contains, "R012100000102").Column("Commodities").Expect("0");


            //check Footer totals are correct
            BelowRow(The.Bottom).LeftOf("13.99 kg").Expect("16.25 kg");
            BelowRow(The.Bottom).RightOf("16.25 kg").Expect("13.99 kg");
            BelowRow(The.Bottom).RightOf("13.99 kg").Expect("750.00");
        }
    }
}
