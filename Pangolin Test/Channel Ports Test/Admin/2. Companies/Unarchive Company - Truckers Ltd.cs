using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveCompany_TruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCompany_TruckersLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            //check company is in list (filtered by 'archived' status) 
            Click("Companies");
            WaitToSeeHeader("Companies");
            Set("Company name").To("");
            ClickLabel("Archived");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Companies");
            ExpectRow(That.Contains, "Truckers Ltd");

            // ----------------------------------------------
            //unarchive company
            ClickLabel("All");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Companies");
            AtRow(That.Contains, "Truckers Ltd").Column(That.Contains, "Archive").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive Reason");
            Click(The.Left, "Unarchive");

            // ----------------------------------------------

            //check company is not in list (filtered by 'archived' status)
            ClickLabel("Active");
            Click("Search");
            ExpectRow(That.Contains, "Truckers Ltd");

            //NOTE: Uncomment the following, once Shipment page and form have been implemented.
            // check can add a Shipment for company
            /*Click("Shipment");
            Click("New Shipment");
            ClickField("Company name");
            Type("Truck");
            ExpectItem("Truckers ltd");*/
        }
    }
}