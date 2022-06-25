using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveContactGroupImport : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveContactGroup_Import>();
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
            ClickLabel("Archived");
            Click("Search");

            // ----------------------------------------------

            // Unarchive
            AtRow("Import").Column("Archive").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive reason");
            ClickButton("Unarchive");

            ExpectNoRow("Import");
        }
    }
}