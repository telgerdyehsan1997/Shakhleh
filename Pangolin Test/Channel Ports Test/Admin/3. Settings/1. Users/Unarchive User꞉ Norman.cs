using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveUserNorman : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveUserNorman>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectNoRow(That.Contains, "Freeman");

            ClickLabel("Archived");
            Click("Search");
            ExpectRow(That.Contains, "Norman");

            // ----------------------------------------------

            //unarchive user - confirm
            AtRow(That.Contains, "Freeman").Column(That.Contains, "Archive").Click("Unarchive");
            ExpectHeader("Unarchive");
            AtLabel("Archive Reason").Expect("ARCHIVE REASON");
            Set("Please Explain Why").To("Unarchive Reason");
            ClickButton("Unarchive");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectNoRow(That.Contains, "Freeman");

            // ----------------------------------------------

            //login as archived user
            Logout();
            Goto("/");
            Set("Email").To("nfreeman@uat.co");
            Set("Password").To("test");
            Click("Login");

            WaitForNewPage();
        }
    }
}