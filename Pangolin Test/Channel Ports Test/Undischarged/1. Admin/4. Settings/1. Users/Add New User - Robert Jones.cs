using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewUserRobertJones : UITest
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
            Set("First name").To("Alice");
            Set("Last name").To("Spat");
            Set("Email").To("aspat@uat.co");
            Set("Phone Number").To("07589456123");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow("Alice");
            AtRow("Alice").Column("First name").Expect("Alice");
            AtRow("Alice").Column("Last name").Expect("Spat");
            AtRow("Alice").Column("Email").Expect("aspat@uat.co");
            AtRow("Alice").Column("Phone Number").Expect("07589456123");
        }
    }
}