using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageTrackingNumber : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddConsignmentForWWL, AddAnotherConsignmentToWWL>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            ClickLabel(The.Top, "All");
            Click("Search");
            AtRow(That.Contains, "T0721000001").Column("Tracking number").ClickLink();
            WaitToSeeHeader("Shipment Details");

            //assert layout
            ExpectHeader("Shipment Details");
            BelowHeader("Shipment Details").ExpectLabel("Company name");
            BelowLabel("Company name").ExpectLabel("Company type");
            BelowLabel("Company type").ExpectLabel("Primary contact");
            BelowLabel("Customer Reference").ExpectLabel("Type");
            BelowLabel("Type").ExpectLabel("Vehicle number");
            BelowLabel("Vehicle number").ExpectLabel("Trailer number");
            BelowLabel("Trailer number").ExpectLabel("Attachment");

            //assert details
            AtLabel("Company name").Expect("Worldwide Logistics Ltd");
            AtLabel("Company type").Expect("Customer");
            AtLabel("Primary contact").Expect("Jenny Smith");
            AtLabel("Customer Reference").Expect("RT564744");
            AtLabel("Type").Expect("Out of UK");
            //AtLabel("Vehicle number").Expect("VH1");
            //AtLabel("Trailer number").Expect("TR1");

            //assert consignments
            AtRow(That.Contains, "T072100000101").Column("Consignment number").Expect("T072100000101");
            AtRow(That.Contains, "T072100000101").Column("UCR").Expect("1GB683470514001-T072100000101");
            AtRow(That.Contains, "T072100000101").Column("UK Trader").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "T072100000101").Column("Partner").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "T072100000101").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "T072100000101").Column("Total packages").Expect("4");
            AtRow(That.Contains, "T072100000101").Column("Total gross weight").Expect("10.25 kg");
            AtRow(That.Contains, "T072100000101").Column("Total net weight").Expect("5.78 kg");
            AtRow(That.Contains, "T072100000101").Column("Invoice number").Expect("WWL-2019-1101");
            AtRow(That.Contains, "T072100000101").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "T072100000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "T072100000101").Column("Commodities").Expect("0");

            AtRow(That.Contains, "T072100000102").Column("Consignment number").Expect("T072100000102");
            AtRow(That.Contains, "T072100000102").Column("UCR").Expect("1GB683470514001-T072100000102");
            AtRow(That.Contains, "T072100000102").Column("UK Trader").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "T072100000102").Column("Partner").Expect("Worldwide Logistics Ltd");
            AtRow(That.Contains, "T072100000102").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "T072100000102").Column("Total packages").Expect("3");
            AtRow(That.Contains, "T072100000102").Column("Total gross weight").Expect("1,000 kg");
            AtRow(That.Contains, "T072100000102").Column("Total net weight").Expect("900 kg");
            AtRow(That.Contains, "T072100000102").Column("Invoice number").Expect("WWL-2019-1101");
            AtRow(That.Contains, "T072100000102").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "T072100000102").Column("Total value").Expect("300.00");
            AtRow(That.Contains, "T072100000102").Column("Commodities").Expect("0");
        }
    }
}
