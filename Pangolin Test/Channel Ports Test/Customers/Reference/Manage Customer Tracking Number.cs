using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCustomerTrackingNumber : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd>();

            //navigate
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set("Date Created").To("28/06/2018");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");

            AtRow(That.Contains, "R0119000001").Column("Tracking number").ClickLink("R0119000001");
            WaitToSeeHeader("Shipment Details");

            //assert layout
            ExpectHeader("Shipment Details");
            BelowHeader("Shipment Details").ExpectLabel("Company name");
            BelowLabel("Company name").ExpectLabel("Company type");
            BelowLabel("Company type").ExpectLabel("Primary contact");
            BelowLabel("Primary contact").ExpectNoLabel("Contact type");
            BelowLabel("Primary contact").ExpectNoLabel("Group");
            BelowLabel("Primary contact").ExpectLabel("Customer Reference");
            BelowLabel("Customer Reference").ExpectLabel("Type");
            BelowLabel("Type").ExpectLabel("Vehicle number");
            BelowLabel("Vehicle number").ExpectLabel("Trailer number");
            BelowLabel("Trailer number").ExpectLabel("Expected date of arrival");

            //assert details
            AtLabel("Company name").Expect("Truckers Ltd");
            AtLabel("Company type").Expect("Customer");
            AtLabel("Primary contact").Expect("Jim Stevens");
            AtLabel("Customer Reference").Expect("55555");
            AtLabel("Type").Expect("Into UK");
            //AtLabel("Vehicle number").Expect(What.Contains, "89A");
            AtLabel("Trailer number").Expect("6514");
            ExpectNoLabel("Upload attachment");
            ExpectNoText("Download");

            //assert consignments
            AtRow(That.Contains, "R011900000101").Column("Consignment number").Expect("R011900000101");
            AtRow(That.Contains, "R011900000101").Column("UK trader").Expect("Truckers Ltd");
            AtRow(That.Contains, "R011900000101").Column("Partner").Expect("TRUCKERS LTD");
            AtRow(That.Contains, "R011900000101").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "R011900000101").Column("Total packages").Expect("6");
            AtRow(That.Contains, "R011900000101").Column("Total gross weight").Expect("12 kg");
            AtRow(That.Contains, "R011900000101").Column("Total net weight").Expect("9 kg");
            AtRow(That.Contains, "R011900000101").Column("Invoice number").Expect("TRUCKERS-2019-0001");
            AtRow(That.Contains, "R011900000101").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R011900000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "R011900000101").Column("Commodities").Expect("0");
            AtRow(That.Contains, "R011900000101").Column("Import Entry").ExpectLink("View");

        }
    }
}