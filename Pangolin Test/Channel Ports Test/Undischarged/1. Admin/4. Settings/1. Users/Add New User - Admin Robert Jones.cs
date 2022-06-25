using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAdminUser_RobertJones : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<Undischarged_ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            ExpectHeader("Undischarged NCTS");
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");

            // ----------------------------------------------

            //create new user
            Click("New User");
            WaitToSeeHeader(That.Contains, "User Details");
            Set("First name").To("Admin");
            Set("Last name").To("Spat");
            Set("Email").To("Yoyoadmin@uat.co");
            Set("Phone Number").To("07589456123");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow("Alice");
            AtRow("Alice").Column("First name").Expect("Admin");
            AtRow("Alice").Column("Last name").Expect("Spat");
            AtRow("Alice").Column("Email").Expect("Yoyoadmin@uat.co");
            AtRow("Alice").Column("Phone Number").Expect("07589456123");
            AtRow("Alice").Column("Admin").ExpectTick();
        }
    }
}