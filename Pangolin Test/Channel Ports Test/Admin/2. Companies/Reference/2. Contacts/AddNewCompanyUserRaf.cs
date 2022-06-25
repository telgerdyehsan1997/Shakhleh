using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewCompanyUserRaf : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactForGeeksQA_TestContact>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("GEEKSQA");
            AtRow("GEEKSQA").Column("Company name").ClickLink("");
            Click("Company Users");
            WaitToSeeHeader("Company Users");
            Click("New Company User");

            // ----------------------------------------------

            // Create new company user
            ExpectHeader("Company User Details");
            Set("First name").To("Rafal");
            Set("Last name").To("QA");
            Set("Email address").To("rafal.misko@geeks.ltd.uk");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            AtLabel("Accounts department").ClickLabel("Yes");
            AtLabel("Customer Admin").ClickLabel("Yes");
            Click("Save");

            ExpectText("rafal.misko@geeks.ltd.uk");

            Click("Contact Groups");
            Click("New Contact Group");
            ExpectHeader("Contact Group Details");
            Set("Group name").To("Email me");
            Click("Save");

            AtRow("EMAIL ME").Column("Contacts").ClickLink();

            AtRow(That.Contains, "TEST").ClickCheckbox();
            Click("Save");

            AtRow("EMAIL ME").Column("Contacts").Expect("1");


            //-----------Set password------------------------

            Logout();
            Goto("/");
            ExpectHeader("Please Login");
            Click("Forgot password");
            WaitToSeeHeader(That.Contains, "Forgot password");
            BelowHeader("Forgot password").Expect("Please enter your email address below and press Send button. We will send you an email with a link to reset your password:");
            Below("Please enter your email address below and press Send button. We will send you an email with a link to reset your password:").Expect("Email");
            Set(The.Top, "Email").To("rafal.misko@geeks.ltd.uk");
            Click("Cancel");
            CheckMailBox("rafal.misko@geeks.ltd.uk");

            Click("Welcome to Channel Ports");
            Click("Set Your Password");
            ExpectHeader("Set Your Password");
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            Click("Save");
            ExpectHeader("RAFAL QA");


        }
    }
}