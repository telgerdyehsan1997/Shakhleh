using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ForgotPassword : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //cancel forgot password
            Logout();
            Goto("/");
            ExpectHeader("Please Login");
            Click("Forgot password");
            WaitToSeeHeader(That.Contains, "Forgot password");
            BelowHeader("Forgot password").Expect("Please enter your email address below and press Send button. We will send you an email with a link to reset your password:");
            Below("Please enter your email address below and press Send button. We will send you an email with a link to reset your password:").Expect("Email");
            Set(The.Top, "Email").To("admin@uat.co");
            Click("Cancel");
            CheckMailBox("admin@uat.co");
            ExpectNo("Channel Port - Recover Password");

            //invalid email address test
            Goto("/");
            ExpectHeader("Please Login");
            Click("Forgot password");
            WaitToSeeHeader(That.Contains, "Forgot password");
            Set(The.Top, "Email").To("test@uat.co");
            Click("Send");
            Expect("Invalid email address. Please try again.");
            Click("OK");

            //cancel password change after clicking recovery link
            Set(The.Top, "Email").To("admin@uat.co");
            Click("Send");

            // ----------------------------------------------

            //click recovery link
            CheckMailBox("admin@uat.co");
            Click("Channel Ports - Recover Password");
            Expect(What.Contains, "Dear Geeks Admin,");
            Expect(What.Contains, "Please click on the following link to reset your password. If you did not request this password reset then please contact us.");
            Click("Reset Your Password");
            WaitToSeeHeader(That.Contains, "Reset Your Password");
            Set("Password").To("test2");
            Set("Confirm New Password").To("test2");
            Click("Cancel");
            WaitToSeeHeader(That.Contains, "Please Login");

            // ----------------------------------------------

            //check new password was not set
            Set("Email").To("admin@uat.co");
            Set("Password").To("test2");
            Click("Login");
            WaitToSee(What.Contains, "The email or password was incorrect");
            Click("OK");

            // ----------------------------------------------

            //check old password still works
            RefreshPage();
            Set("Email").To("admin@uat.co");
            Set("Password").To("test");
            Click("Login");
            ExpectHeader("Shipments");

            //WaitToSeeHeader(That.Contains, "Users"); //THIS WILL NEED UPDATING TO PRE-ADVICE ONCE DEVELOPED

            // ----------------------------------------------

            //change password
            Logout();
            CheckMailBox("admin@uat.co");
            Click("Channel Ports - Recover Password");
            Click("Reset Your Password");
            WaitToSeeHeader(That.Contains, "Reset Your Password");
            Set("Password").To("test2");
            Set("Confirm New Password").To("test2");
            Click("Reset");

            Expect(What.Contains, "Your password has been successfully reset.");

            //try login with old password 'test'
            Click("Proceed to the login page.");
            WaitToSeeHeader("Please Login");
            Set("Email").To("admin@uat.co");
            Set("Password").To("test");
            Click("Login");
            WaitToSee(What.Contains, "The email or password was incorrect");
            Click("OK");

            // ----------------------------------------------

            //login with new password 'test2'
            Logout();
            Goto("/");
            WaitToSeeHeader("Please Login");
            Set("Email").To("admin@uat.co");
            Set("Password").To("test2");
            Click("Login");
            ExpectHeader("Shipments");

            //WaitToSeeHeader(That.Contains, "Users"); //UPDATE THIS TO READ PRE-ADVICE AFTER DEVELOPED
        }
    }
}