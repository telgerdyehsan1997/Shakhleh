using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewContactForGeeksQARafalQA : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyGeeksQA>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("GeeksQA");
            AtRow("GeeksQA").Column("Company name").ClickLink("");
            Click("Company Users");
            WaitToSeeHeader("Company Users");
            Click("New Company User");

            // ----------------------------------------------

            // Create new company user
            ExpectHeader("Company User Details");
            Set("First name").To("Rafal");
            Set("Last name").To("QA");
            Set("Email address").To("rafalqa@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            AtLabel("Accounts department").ClickLabel("Yes");
            AtLabel("Customer Admin").ClickLabel("Yes");
            Click("Save");

            ExpectText("rafalqa@uat.co");

            //-----------Set password------------------------

            Logout();
            Goto("/");
            ExpectHeader("Please Login");
            Click("Forgot password");
            WaitToSeeHeader(That.Contains, "Forgot password");
            BelowHeader("Forgot password").Expect("Please enter your email address below and press Send button. We will send you an email with a link to reset your password:");
            Below("Please enter your email address below and press Send button. We will send you an email with a link to reset your password:").Expect("Email");
            Set(The.Top, "Email").To("rafalqa@uat.co");
            Click("Cancel");
            CheckMailBox("rafalqa@uat.co");

            Click("Welcome to Channel Ports");
            Click("Set Your Password");
            ExpectHeader("Set Your Password");
            System.Threading.Thread.Sleep(1000);
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            Click("Save");
            ExpectHeader("RAFAL QA");


        }
    }
}