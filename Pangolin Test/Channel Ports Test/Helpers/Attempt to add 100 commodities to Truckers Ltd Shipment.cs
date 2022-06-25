using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AttemptToAdd100CommoditiesToTruckersLtdShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProduct_IPad, AddNewShipmentForTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/01/2020");
            Goto("/");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0120000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click(What.Contains, "Save");
            //Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments - Into UK");
            Click("New Consignment");

            //check validation tooltips
            WaitForNewPage();
            Click("Save and Add Commodities");
            Expect("The UK trader field is required.");
            Expect("The Partner field is required.");
            //default declarant is set
            ExpectNo("The Declarant field is required.");
            Expect("The Total packages field is required.");
            Expect("The Total gross weight field is required.");
            Expect("The Total net weight field is required.");
            Expect("The Invoice number field is required.");
            Expect("The Invoice currency field is required.");
            Expect("The Total value field is required.");
            Click("Cancel");
            WaitForNewPage();
            Click("New Consignment");

            //add new con

            WaitToSee("Consignment Details");
            AtLabel("Consignment number").Expect("R012000000101");
            ClickField("UK trader");
            Type("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            ClickField("Partner name");
            Type("Worldwide Logistics Ltd - Worcester - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);

            // default declarant working
            AtLabel("Declarant").ExpectValue(That.Contains, "Imports");
            ClickLabel("Declarant");
            Click(The.Bottom, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.251");
            Set("Total net weight").To("4.9911");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice currency");
            Type("GB");
            System.Threading.Thread.Sleep(3000);
            Click(What.Equals, "GBP");
            Set("Total value").To("300");
            Set("UCR").To("9GBGB222333444555-R012000000101");
            Click(What.Contains, "Save");

            CreateCommodity();

            Click("New Commodity");
            Set("Product code").To("IP");
            Click(What.Contains, "IPad");
            Set("Gross Weight").To("12");
            Set("Net weight").To("12.0");
            Set("Value").To("100");
            Set("Number of packages for this commodity code (if known)").To("15");
            Set("Country of origin").To("Fran");
            Click(What.Contains, "France");
            Click("Save");
            Expect("A consignment may only have up to 99 commodities. Please remove an existing commodity before adding a new one");
        }

        private void CreateCommodity()
        {
            for (int i = 0; i <= 98; i++)
            {
                Click("New Commodity");
                Set("Product code").To("IP");
                Click(What.Contains, "IPad");
                Set("Gross Weight").To("12");
                Set("Net weight").To("12.0");
                Set("Value").To("100");
                Set("Number of packages for this commodity code (if known)").To("15");
                Set("Country of origin").To("Fran");
                Click(What.Contains, "France");
                Click("Save");
                ExpectHeader(That.Contains, "R012000000101 - Commodities");
            }
        }
    }
}
