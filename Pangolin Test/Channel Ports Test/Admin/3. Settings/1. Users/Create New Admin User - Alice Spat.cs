using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewAdminUser_AliceSpat : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            
            // ----------------------------------------------

            // Navigation
            ExpectHeader("Shipments");
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");

            // ----------------------------------------------

            //create new user
            Click("New User");
            WaitToSeeHeader(That.Contains, "User Details");
            Set("First name").To("Alice");
            Set("Last name").To("Spat");
            Set("Email").To("aspat@uat.co");
            Set("Phone Number").To("07589456123");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow("Alice");
            AtRow("Alice").Column("First name").Expect("Alice");
            AtRow("Alice").Column("Last name").Expect("Spat");
            AtRow("Alice").Column("Email").Expect("aspat@uat.co");
            AtRow("Alice").Column("Phone Number").Expect("07589456123");
            AtRow("Alice").Column("Admin").ExpectTick();

            // ----------------------------------------------

            //Set Password
            CheckMailBox("aspat@uat.co");
            ExpectText("Welcome to Channel Ports");
            ClickText("Welcome to Channel Ports");
            WaitToSeeHeader(That.Contains, "Welcome to Channel Ports");
            Click("Set Your Password");
            WaitToSeeHeader("Set Your Password");
            
            System.Threading.Thread.Sleep(1000);
            Set(That.Equals, "Password").To("test");
            Set("Confirm new password").To("test");
            Click("Save");

            WaitToSeeHeader("Alice Spat");
            Expect(What.Contains, "Your password has been successfully set.");

            //--------------------------------------------------------
            //Login as aboce user

            Logout();
            LoginAs<ChannelPortsAdmin>();

            Expect("Shipments");
            ClickLink("Settings");
            ExpectHeader("Users");

            AtRow("ALICE").Column("Phone number").Expect("07589456123");



        }
    }
}