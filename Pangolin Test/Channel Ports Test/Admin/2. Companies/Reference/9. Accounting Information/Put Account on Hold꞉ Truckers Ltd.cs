using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class PutAccountOnHoldTruckersLtd : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAccountingInformationTruckersLtd,JohnSmithCreatesACustomerAccount>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            Click("Accounting Information");
            ExpectHeader("Accounting Information");

            ExpectNo("Activate Account");
            Click("Place Account on Hold");
            ExpectText("Are you sure you want to place this account on hold?");
            Click("OK");
            ExpectNo("Place Account on Hold");
            Expect("Activate Account");

            Logout();
            Logout();
            AssumeDate("01/07/2019");

            Goto("/");
            Set("Email").To("JohnSmith@uat.co");
            Set("Password").To("test");
            Click("Login");
            ExpectText("Your company is currently on hold");
        }
    }
}
