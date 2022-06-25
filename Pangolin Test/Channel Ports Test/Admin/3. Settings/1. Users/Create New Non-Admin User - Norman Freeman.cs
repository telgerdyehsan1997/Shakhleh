using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewNon_AdminUser_NormanFreeman : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            ClickLink("Settings");
            WaitToSeeHeader(That.Contains, "Users");

            // ----------------------------------------------

            //new user
            Click("New User");
            WaitToSeeHeader(That.Contains, "User Details");
            Set("First name").To("Norman");
            Set("Last name").To("Freeman");
            Set("Email").To("nfreeman@uat.co");
            AtLabel("Admin").ClickLabel("No");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow("Norman");
            AtRow("Norman").Column("First name").Expect("Norman");
            AtRow("Norman").Column("Last name").Expect("Freeman");
            AtRow("Norman").Column("Email").Expect("nfreeman@uat.co");
            AtRow("Norman").Column("Admin").ExpectNoTick();

            // ----------------------------------------------

            //Set Password
            CheckMailBox("nfreeman@uat.co");
            ExpectText("Welcome to Channel Ports");
            ClickText("Welcome to Channel Ports");
            WaitToSeeHeader(That.Contains, "Welcome to Channel Ports");
            Click("Set Your Password"); //This should be Set password?

            WaitToSeeHeader("Set Your Password");
            System.Threading.Thread.Sleep(1000);
            Set(That.Equals, "Password").To("test");
            Set("Confirm new password").To("test");
            Click("Save");

            Expect(What.Contains, "Your password has been successfully set.");
        }
    }
}