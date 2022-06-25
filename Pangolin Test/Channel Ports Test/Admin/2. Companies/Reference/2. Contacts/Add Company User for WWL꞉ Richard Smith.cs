using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyUserForWWLRichardSmith : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyWorldwideLogisticsLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Company name").ClickLink("");
            Expect("Company Users");
            Click("Company Users");
            WaitToSeeHeader("Company Users");
            Click("New Company User");

            // ----------------------------------------------

            // Create new company user
            Set("First name").To("Richard");
            Set("Last name").To("Smith");
            Set("email address").To("richardsmith@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            AtLabel("Accounts department").ClickLabel("Yes");
            AtLabel("Customer Admin").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Richard");
            AtRow("Richard").Column("Last name").Expect("Smith");
            AtRow("Richard").Column("Email address").Expect("richardsmith@uat.co");
            AtRow("Richard").Column("Telephone number").Expect("12345678909");
            AtRow("Richard").Column("Mobile number").Expect("07986543214");
            // ----------------------------------------------

            //Set Password
            CheckMailBox("richardsmith@uat.co");
            ExpectText("Welcome to Channel Ports");
            ClickText("Welcome to Channel Ports");
            WaitToSeeHeader(That.Contains, "Welcome to Channel Ports");
            Click("Set Your Password");

            ExpectHeader("Set Your Password");
            System.Threading.Thread.Sleep(1000);
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            Click("Save");

            WaitToSeeHeader("Richard Smith");
            Expect(What.Contains, "Your password has been successfully set.");
        }
    }
}