using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchivedCountryNotVisibleInCustomerConsignmentForm : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JennySmithAddsShipmentForWWL_OutOfUK,ArchiveCountryUK>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("01/02/2020");
            Goto("/");

            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Shipments Out of UK");
            NearLabel("Status").ClickLabel("All");
            Click("Search");
            AtRow("T0220000001").Click("Edit");
            Click("Save and Add/Amend Consignments");

            // add consignment
            Click("New consignment");
            AtLabel("Consignment number").Expect("T022000000101");
            // there is no field country of destination
            ClickField("Country of destination");
            Type("United");
            Expect(What.Contains, "Not Found");
        }
    }
}
