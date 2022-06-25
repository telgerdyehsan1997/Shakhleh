using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveUserNorman : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewNon_AdminUser_NormanFreeman>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitToSeeHeader("Users");
            ExpectRow("Norman");

            // ----------------------------------------------

            //archive user - cancel
            AtRow(That.Contains, "Freeman").Column(That.Contains, "Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            ClickButton("Cancel");
            ExpectRow("Freeman");

            // ----------------------------------------------

            //archive user - confirm
            AtRow(That.Contains, "Freeman").Column(That.Contains, "Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectNoRow(That.Contains, "Freeman");

            // ----------------------------------------------

            //login as archived user
            Logout();
            Goto("/");
            Set("Email").To("nfreeman@uat.co");
            Set("Password").To("test");
            Click("Login");

            Expect("The email or password was incorrect");
            Click("OK");
        }
    }
}