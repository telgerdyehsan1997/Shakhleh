using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewContactForTruckers_AlanSmith : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Column("Company name").ClickLink("");
            Expect("Contacts");
            Click("Contacts");
            WaitToSeeHeader("Contacts");
            Click("New Contact");

            // ----------------------------------------------

            // Create new company user
            Set("First name").To("Alan");
            Set("Last name").To("Smith");
            Set("Email address").To("alansmith@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            Click("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Alan");
            AtRow("Alan").Column("Last name").Expect("Smith");
            AtRow("Alan").Column("Email address").Expect("alansmith@uat.co");
            AtRow("Alan").Column("Telephone number").Expect("12345678909");
            AtRow("Alan").Column("Mobile number").Expect("07986543214");
            // ----------------------------------------------

            // Expect no email sent for a contact
            CheckMailBox("alansmith@uat.co");
            ExpectNoText("Welcome to Channel Ports");

            Goto("/");

            // Contacts cannot get Reset Password emails
            Click("Logout");

            Click("Forgot password");

            WaitToSeeHeader("Forgot password");

            Set(The.Top, "Email").To("alansmith@uat.co");
            Click("Send");

            Expect(What.Contains, "Invalid email address. Please try again.");
        }
    }
}