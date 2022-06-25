using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TruckersUsedInConsignmentForWWLSoWWLNotVisible : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112152")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            // add consignment for WWL - use truckers ltd - WWL not visible when truckers adding consignment

            Run<AddShipmentForWWL, JohnSmithAddsShipmentForTruckersLtd>();
            // add consignment for WWL as admin
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("01/01/2019").Click("Edit");
            Click("Save and Add/Amend Consignments");

            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("3");
            Set("Total gross weight").To("5.251");
            Set("Total net weight").To("4.9911");
            Set("Invoice number").To("TRUCKERS-2019-1101");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Total value").To("300");

            Set("Terms of Sale").To("EXW - Ex Works");

            Click("Save and Add Commodities");

            // 
            LoginAs<JohnSmithCustomer>();

            ExpectHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("01/02/2022").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            //Workflow no longer exists
            /*
            // change WWL to default declarant
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            Click("Global settings");
            Set("Default declarant").To("Worldwide logistics Ltd");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "Worldwide Logistics");
            Click(What.Contains, "Save"); */


            ClickLink("Shipments Into UK");
            ExpectHeader("Shipments Into UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            /*  AtRow("01/01/2019").Column("Edit").Click("Edit");
              ExpectHeader("Shipment Details");
              Click("Save and Add/Amend Consignments");


              ExpectHeader("Consignment Details");
              Set("UK Trader").To("");
              ClickField("UK Trader");
              Type("Worldwide");
              System.Threading.Thread.Sleep(2000);
              Expect(What.Contains, "Worldwide");

              Press(Keys.Tab);

              Set("Partner name").To("");
              ClickField("Partner name");
              Type("Worldwide");
              System.Threading.Thread.Sleep(2000);
              Expect(What.Contains, "Worldwide");

              Press(Keys.Tab);

              Set("Declarant").To("");
              ClickField("Declarant");
              Type("Worldwide");
              System.Threading.Thread.Sleep(2000);
              Expect(What.Contains, "Worldwide"); */
        }
    }
}
