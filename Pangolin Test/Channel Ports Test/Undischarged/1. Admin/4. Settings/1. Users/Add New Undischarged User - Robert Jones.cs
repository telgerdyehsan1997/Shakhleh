using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewUndischargedUser_RobertJones : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Users";

            LoginAs<Undischarged_ChannelPortsAdmin>();

            //Navigates to Users
            this.NavigateToUDSettings(settingsPage);

            //Creates the New User
            ClickLink("New User");
            ExpectHeader("User Details");
            Set("First name").To("Robert");
            Set("Last name").To("Jones");
            Set("Email").To("robert.jones@uat.co");
            AtLabel("Admin").ClickLabel("Yes");
            ClickButton("Save");

            //Asserts that new User has been saved
            ExpectRow("Robert");
            AtRow("Robert").Column("Admin").ExpectTick();

            //Adds password for User
            CheckMailBox("robert.jones@uat.co");
            AtRow("robert.jones@uat.co").Column("Subject").ClickLink();
            ExpectHeader("Subject: Welcome to Channel Ports");
            ClickLink("Set Your Password");
            ExpectHeader("Set Your Password");
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            ClickButton("Save");
            Expect(What.Contains, "Your password has been successfully set");
        }
    }
}