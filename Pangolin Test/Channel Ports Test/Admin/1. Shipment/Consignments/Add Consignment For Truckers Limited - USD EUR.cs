using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentsForTruckersLimited_USD_EUR : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProduct_IPad, AddShipmentForTruckersLtd_OutOfUK>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("04/10/1999");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "T0721000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader("Consignment Details");

            //add new con

            AtLabel("Consignment number").Expect("T072100000101");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            // default declarant working
            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CHANNEL PORTS - HYTHE - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CHANNEL PORTS - HYTHE - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Total value").To("300");
            Set("Total packages").To("3");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.991");
            Set("Invoice number").To("TRUCKERS-2019-1102");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FAS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FAS");
            Click(What.Contains, "Save");
            //EXPECT
            //

            //assert new details
            Click("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "RT564744").Column("Edit").Click("Edit");
            Click(What.Contains, "Save");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Consignment number").Expect(What.Contains, "T072100000101");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("UCR").Expect(What.Contains, "1GB683470514001-T072100000101");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("UK Trader").Expect("Channel Ports");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Partner").Expect("TRUCKERS LTD");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total packages").Expect("3");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total gross weight").Expect("5.25 kg");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total net weight").Expect("4.991 kg");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Invoice number").Expect("TRUCKERS-2019-1102");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Total value").Expect("300.00");
            AtRow(That.Contains, "TRUCKERS-2019-1102").Column("Commodities").Expect("0");


            Click("New Consignment");

            //add new con

            WaitToSee("Consignment Details");

            // default declarant working
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("500");
            Set("Total packages").To("1");
            Set("Total gross weight").To("5.25");
            Set("Total net weight").To("4.991");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FAS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FAS");
            Click(What.Contains, "Save");
            ExpectHeader(That.Contains, "Commodities");
            //assert new details
            Click("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow("T0721000001").Column("Tracking number").Click("T0721000001");
            // AtRow(That.Contains, "TRUCKERS-2019-1101").Column("UCR").Expect("1GB683470514001-T072100000101");
            AtRow("T072100000101").Column("UK Trader").Expect("Channel Ports");
            AtRow("T072100000101").Column("Partner").Expect("TRUCKERS LTD");
            AtRow("T072100000101").Column("Declarant").Expect("CHANNEL PORTS");
            AtRow("T072100000101").Column("Total packages").Expect("3");
            AtRow("T072100000101").Column("Total gross weight").Expect("5.25 kg");
            AtRow("T072100000101").Column("Total net weight").Expect("4.991 kg");
            AtRow("T072100000101").Column("Invoice number").Expect("TRUCKERS-2019-1102");
            AtRow("T072100000101").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow("T072100000101").Column("Total value").Expect("300.00");
            AtRow("T072100000101").Column("Commodities").Expect("0");
        }
    }
}
