using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Add2ndConsignmentToTruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<AddCompanyAlpha, AddCompanyDelta, AddConsignmentToTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            this.FindShipment(trackingNumber);
            AtRow(That.Contains, "R0721000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments - Into UK");
            Click("New Consignment");

            //add new con
            ExpectHeader("Consignment Details");
            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect("ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click("ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect("DELTA LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654322");
            System.Threading.Thread.Sleep(1000);
            Click("DELTA LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654322");

            // default declarant working
            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect("Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click("Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            //ClickField("Declarant");
            //Type(" - Worcester - WR5 3DA - GB987654312000");
            //System.Threading.Thread.Sleep(3000);
            //Press(Keys.ArrowDown);
            //Press(Keys.Enter);
            Set("Total packages").To("10");
            Set("Total gross weight").To("17");
            Set("Total net weight").To("10");
            Set("Invoice number").To("TRUCKERS-2019-1101-02");
            // ClickField("Invoice currency");
            // Type("GB");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect("Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Terms of Sale").To("EXW - Ex Works");
            Set("Freight currency").To("Great Britain - GBP");
            Set("Freight amount").To("1");

            Set("Total value").To("750");
            Click(What.Contains, "Save");
            Click("Back");
            Click("Cancel");

            //assert new details
            AtRow(That.Contains, "R072100000102").Column("Consignment number").Expect("R072100000102");
            AtRow(That.Contains, "R072100000102").Column("UCR").Expect("1GB683470514001-R072100000102");
            AtRow(That.Contains, "R072100000102").Column("UK Trader").Expect("Alpha Ltd");
            AtRow(That.Contains, "R072100000102").Column("Partner").Expect("Delta Ltd");
            AtRow(That.Contains, "R072100000102").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "R072100000102").Column("Total packages").Expect("10");
            AtRow(That.Contains, "R072100000102").Column("Total gross weight").Expect("17 kg");
            AtRow(That.Contains, "R072100000102").Column("Total net weight").Expect("10 kg");
            AtRow(That.Contains, "R072100000102").Column("Invoice number").Expect("TRUCKERS-2019-1101-02");
            AtRow(That.Contains, "R072100000102").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R072100000102").Column("Total value").Expect("750.00");
            AtRow(That.Contains, "R072100000102").Column("Commodities").Expect("0");
        }
    }
}
