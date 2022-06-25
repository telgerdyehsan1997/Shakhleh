using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageContactGroups : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewContactGroup_Export,ArchiveContactGroup_Import>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader("Truckers Ltd");
            Click("Contact Groups");
            WaitToSeeHeader("Contact Groups");

            // ----------------------------------------------

            // Search active
            ClickLabel("Active");
            Click("Search");

            ExpectRow("Export");
            ExpectNoRow("Import");

            // ----------------------------------------------

            // Search archived
            ClickLabel("Archived");
            Click("Search");

            ExpectRow("Import");
            ExpectNoRow("Export");

            // ----------------------------------------------

            // Search all
            ClickLabel("All");
            Click("Search");

            ExpectRow("Export");
            ExpectRow("Import");
        }
    }
}
