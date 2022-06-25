using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveCompany_TruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            //Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //archive company
            WaitToSeeHeader(That.Contains, "Companies");
            AtRow(That.Contains, "Truckers Ltd").Column(That.Contains, "Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("TRUCKERS LTD");

            // ----------------------------------------------

            //check company is in list (filtered by 'all' status)
            Set("Company name").To("");
            ClickLabel("All");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Companies");
            ExpectRow(That.Contains, "Truckers Ltd");

            // ----------------------------------------------

            //check company is not in list (filtered by 'active' status) 
            Set("Company name").To("");
            ClickLabel("Active");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Companies");
            ExpectNoRow(That.Contains, "Truckers Ltd");

            // check can't add a Shipment for company
            Click("Shipments");
            Click("New Shipment");
            ClickField("Company name");
            Type("Truck");
            ExpectNoItem("Truckers ltd");
        }
    }
}
