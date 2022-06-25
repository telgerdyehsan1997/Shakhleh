using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportRoutingItineraries : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Routing Itineraries Import");
            WaitToSeeHeader("Routing Itineraries Imports");

            // ----------------------------------------------

            Click("New Import");
            WaitToSeeHeader("Upload Routing Itineraries");
            Set("Choose file").To("RoutingItineraries.csv");
            System.Threading.Thread.Sleep(2000);
            Click("Save");

            // ----------------------------------------------

            WaitToSeeHeader("Routing Itineraries Imports");
            AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Routing Itineraries Import Service").Click("Execute");
            Goto("/");

            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Routing Itineraries Import");
            WaitToSeeHeader("Routing Itineraries Imports");
            AtRow("Download").Column("Import status").Expect("Successful");

        }
    }
}