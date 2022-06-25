using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyUserForWWLJenny : UITest
    {
        [TestProperty("Sprint", "1")]

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
            Set("First name").To("Jenny");
            Set("Last name").To("Smith");
            Set("email address").To("jennysmith@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            AtLabel("Accounts department").ClickLabel("No");
            AtLabel("Customer Admin").ClickLabel("No");
            ClickButton("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Jenny");
            AtRow("Jenny").Column("Last name").Expect("Smith");
            AtRow("Jenny").Column("Email address").Expect("jennysmith@uat.co");
            AtRow("Jenny").Column("Telephone number").Expect("12345678909");
            AtRow("Jenny").Column("Mobile number").Expect("07986543214");
            // ----------------------------------------------

            //Set Password
            CheckMailBox("jennysmith@uat.co");
            AtRow("jennysmith@uat.co").Column("Subject").Expect("Welcome to Channel Ports");
            AtRow("jennysmith@uat.co").Column("Subject").ClickLink("Welcome to Channel Ports");
            WaitToSeeHeader(That.Contains, "Welcome to Channel Ports");
            Click("Set Your Password");

            WaitToSeeHeader("Set Your Password");
            ClickLabel("Password");
            Set(That.Equals, "Password").To("test");
            ClickLabel("Confirm new password");
            Set("Confirm new password").To("test");
            Click("Save");

            WaitToSeeHeader("Jenny Smith");
            Expect(What.Contains, "Your password has been successfully set.");
        }
    }
}