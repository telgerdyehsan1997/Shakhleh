using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ConsignmentListsShowWeightAndValueTotalsInFooter : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyUserForWWLJenny, AddAnotherConsignmentToWWL>();
            //check Admin > Edit > SaveAndAddConsignments List
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader(That.Contains, "Shipment");
            NearLabel("Into UK").ClickLabel(The.Top, "All");
            Set("Date created").To("01/01/2020");
            Set("Expected date of arrival/departure").To("01/01/2020");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "T0721000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments - Out of UK");
            AtRow(That.Contains, "T072100000101").Column("Total gross weight").Expect("10.25 kg");
            AtRow(That.Contains, "T072100000102").Column("Total gross weight").Expect("1,000 kg");
            AtRow(That.Contains, "T072100000101").Column("Total net weight").Expect("5.78 kg");
            AtRow(That.Contains, "T072100000102").Column("Total net weight").Expect("900 kg");
            AtRow(That.Contains, "T072100000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "T072100000102").Column("Total value").Expect("300.00");

            ClickLink("Shipments");
            WaitToSeeHeader(That.Contains, "Shipment");
            NearLabel("Into UK").ClickLabel(The.Top, "All");
            Click("Search");
            AtRow(That.Contains, "T0721000001").Column("Tracking number").ClickLink();
            WaitToSeeHeader("Shipment Details");
            AtRow(That.Contains, "T072100000101").Column("Total gross weight").Expect("10.25 kg");
            AtRow(That.Contains, "T072100000102").Column("Total gross weight").Expect("1,000 kg");
            AtRow(That.Contains, "T072100000101").Column("Total net weight").Expect("5.78 kg");
            AtRow(That.Contains, "T072100000102").Column("Total net weight").Expect("900 kg");
            AtRow(That.Contains, "T072100000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "T072100000102").Column("Total value").Expect("300.00");

            //check Customer > Edit > SaveAndAddConsignments List
            LoginAs<JennySmithCustomer>();
            ClickLink("Shipments Out of UK");
            Set("Date created").To("01/01/2020");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "T0721000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments - Out of UK");
            AtRow(That.Contains, "T072100000101").Column("Total gross weight").Expect("10.25 kg");
            AtRow(That.Contains, "T072100000102").Column("Total gross weight").Expect("1,000 kg");
            AtRow(That.Contains, "T072100000101").Column("Total net weight").Expect("5.78 kg");
            AtRow(That.Contains, "T072100000102").Column("Total net weight").Expect("900 kg");
            AtRow(That.Contains, "T072100000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "T072100000102").Column("Total value").Expect("300.00");
            BelowRow(The.Bottom).LeftOf("905.78 kg").Expect("1010.25 kg");
            BelowRow(The.Bottom).RightOf("1010.25 kg").Expect("905.78 kg");
            BelowRow(The.Bottom).RightOf("905.78 kg").Expect("1,500.00");

            //check Customer > [Shipment Details]
            LoginAs<JennySmithCustomer>();
            ClickLink("Shipments Out of UK");
            Set("Date created").To("01/01/2020");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "T0721000001").Column("Tracking number").ClickLink();
            WaitToSeeHeader("Shipment Details");
            AtRow(That.Contains, "T072100000101").Column("Total gross weight").Expect("10.25 kg");
            AtRow(That.Contains, "T072100000102").Column("Total gross weight").Expect("1,000 kg");
            AtRow(That.Contains, "T072100000101").Column("Total net weight").Expect("5.78 kg");
            AtRow(That.Contains, "T072100000102").Column("Total net weight").Expect("900 kg");
            AtRow(That.Contains, "T072100000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "T072100000102").Column("Total value").Expect("300.00");
            BelowRow(The.Bottom).LeftOf("905.78 kg").Expect("1010.25 kg");
            BelowRow(The.Bottom).RightOf("1010.25 kg").Expect("905.78 kg");
            BelowRow(The.Bottom).RightOf("905.78 kg").Expect("1,500.00");
        }
    }
}
