using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class UDForgotPassword : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var userEmail = "undischargedadmin@uat.co";

            LoginAs<Undischarged_ChannelPortsAdmin>();

            //Logs Out of User
            Logout();
            RefreshPage();
            ExpectHeader("Please Login");

            //Sends 'Forgot Password' Email
            Click("Forgot password");
            ExpectHeader("Forgot Password");
            Set(The.Top, "Email").To(userEmail);
            ClickButton("Send");

            //Checks Mailbox for the 'Forgot Password' email
            CheckMailBox(userEmail);
            ExpectRow("Channel Ports - Recover Password");
            AtRow("Channel Ports - Recover Password").Column("Subject").ClickLink();
            ExpectHeader("Subject: Channel Ports - Recover Password");

            //Resets Password
            ClickLink("Reset Your Password");
            ExpectHeader("Reset Your Password");
            Set("Password").To("NewPassword");
            Set("Confirm new password").To("NewPassword");
            ClickButton("Reset");
            Expect(What.Contains, "Your password has been successfully reset");

            ClickLink("Proceed to the login page.");

            //Checks that new Password works
            Set("Email").To(userEmail);
            Set("Password").To("NewPassword");
            ClickButton("Login");
            ExpectLink("Undischarged NCTS");
        }
    }
}