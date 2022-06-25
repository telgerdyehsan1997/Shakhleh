using Pangolin;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentsForTruckersLimitedGBP : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<AdminAddsProduct_IPad, AddShipmentForTruckersLtd_OutOfUK, AdminAddsCompanyWorldwideLogisticsLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();

            this.FindShipment(trackingNumber);

            AtRow(trackingNumber).Column("Consignments").ClickLink();
            ClickLink("New Consignment");
            //Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");
            //add new con
            ClickHeader("Consignment Details");
            Set("UK trader").To("");
            System.Threading.Thread.Sleep(1000);
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            // default declarant working

            AtLabel("Declarant").ExpectValue(That.Contains, "Import");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            //System.Threading.Thread.Sleep(1000);
            //Press(Keys.ArrowDown);
            //Press(Keys.Enter);

            Set("Total value").To("300");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.991");
            Set("Invoice number").To("TRUCKERS-2019-1102");
            Set("Terms of Sale").To("EXW - Ex Works");
            Click(What.Contains, "Save");
            //EXPECT
            //

            //assert new details
            ClickLink("Shipments");
            this.FindShipment(trackingNumber);
            AtRow(That.Contains, "RT564744").Column("Consignments").ClickLink();
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Consignment number").Expect(What.Contains, "T072100000101");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("UCR").Expect(What.Contains, "1IL859098859098-T072100000101");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("UK Trader").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Partner").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Declarant").Expect("Imports Ltd");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total packages").Expect("3");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total gross weight").Expect("5.25 kg");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total net weight").Expect("4.991 kg");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Invoice number").Expect("TRUCKERS-2019-1102");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total value").Expect("300.00");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Commodities").Expect("0");

            // CHANGING 
            //Click("New Consignment");

            ////add new con

            //WaitToSee("Consignment Details");
            //AtLabel("Consignment number").Expect("T071900000102");
            //Set("UK trader").To("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");
            //Click(What.Contains, "Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");
            //Set("Partner name").To("Worldwide Logistics Ltd - Worcester - WR5 3DA - GB683470514001 - 1234567");
            //Click(What.Contains, "Worldwide Logistics Ltd - Worcester - WR5 3DA - GB683470514001 - 1234567");

            //// default declarant working

            //AtLabel("Declarant").ExpectValue(That.Contains, "Import");
            //Set("Invoice currency").To("GBP");
            //Click(What.Equals, "GBP");
            //Set("Total value").To("500");
            //Set("Total packages").To("1");
            //Set("Total gross weight").To("5.25");
            //Set("Total net weight").To("4.991");
            //Set("Invoice number").To("TRUCKERS-2019-1101");
            //Set("UCR").To("9GBGB222333444555-T071900000102");
            //Set("Terms of sale").To("EXW");
            //Click(What.Contains, "Save");
            //ExpectHeader(That.Contains, "Commodities");
            ////assert new details
            //Click("Shipments");
            //Set("to").To("");
            //Click("Search");
            //Click("T0719000001");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("UCR").Expect(What.Contains, "9GBGB222333444555-T07190000010");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("UK Trader").Expect("Truckers Ltd");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Partner").Expect("Worldwide Logistics Ltd");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Declarant").Expect("Imports Ltd");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Total packages").Expect("1");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Total gross weight").Expect("5.25 kg");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Total net weight").Expect("4.991 kg");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Invoice number").Expect("TRUCKERS-2019-1101");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Invoice currency").Expect("EUR");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Total value").Expect("500.00");
            //AtRow(That.Contains, "TRUCKERS-2019-1101").Column("Commodities").Expect("0");
        }
    }
}
