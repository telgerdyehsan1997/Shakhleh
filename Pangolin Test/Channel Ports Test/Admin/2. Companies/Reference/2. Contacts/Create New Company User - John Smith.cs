using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCompanyUser_JohnSmith : UITest
    {
        [TestProperty("Sprint", "1")]

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
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            Expect("Company Users");
            Click("Company Users");
            WaitToSeeHeader("Company Users");
            Click("New Company User");

            // ----------------------------------------------

            // Create new contact
            ExpectHeader("Company User Details");

            Set("First name").To("John");
            Set("Last name").To("Smith");
            Set("email address").To("johnsmith@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            AtLabel("Accounts department").ClickLabel("No");
            AtLabel("Customer Admin").ClickLabel("Yes");

            Click("Save");

            ExpectHeader("Company Users");
            ExpectRowColumns(That.Contains, "JOHN", "SMITH", "JOHNSMITH@UAT.CO", "12345678909", "07986543214", "");
        }
    }
}