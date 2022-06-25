using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCreatesACustomerAccount : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCompanyUser_JohnSmith>();

            AssumeDate("01/07/2019");
            //Set Password
            CheckMailBox("johnsmith@uat.co");
            AtRow("johnsmith@uat.co").ClickLink("Welcome to Channel Ports");
            WaitToSeeHeader(That.Contains, "Welcome to Channel Ports");
            Click("Set Your Password");
            WaitToSeeHeader("Set Your Password");
            ClickLabel("Password");
            Set(That.Equals, "Password").To("test");
            ClickLabel("Confirm new password");
            Set("Confirm new password").To("test");
            Click("Save");
            WaitToSeeHeader("John Smith");
            Expect(What.Contains, "Your password has been successfully set.");

            LoginAs<JohnSmithCustomer>();
            WaitToSee(What.Contains, "NCTS Shipments Out of UK");
        }
    }
}