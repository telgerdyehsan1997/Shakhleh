using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ActivateAccountTruckersLtd : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<PutAccountOnHoldTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            Click("Accounting Information");
            ExpectHeader("Accounting Information");

            Expect("Activate Account");
            ExpectNo("Place Account on Hold");
            Click("Activate Account");
            ExpectText("Are you sure you want to activate this account?");
            Click("OK");
            Expect("Place Account on Hold");
            ExpectNo("Activate Account");

            Logout();

            Goto("/");
            Set("Email").To("JohnSmith@uat.co");
            Set("Password").To("test");
            Click("Login");
            ExpectHeader("Shipments Into UK");
        }
    }
}