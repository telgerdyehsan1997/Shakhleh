using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnArchiveCompanyUserJohn : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCompanyUserJohn>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Truckers Ltd");
            Expect("Company Users");
            Click("Company Users");
            ClickLabel("Archived");
            Click("Search");

            // Unarchive - cancel
            AtRow("John").Click("Unarchive");
            ExpectHeader("Unarchive");
            Click("Cancel");
            ExpectRow("John");

            // Unarchive - confirm
            AtRow("John").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive User");
            Click(The.Left, "Unarchive");
            ExpectNoRow("John");
        }
    }
}