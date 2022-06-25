using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ChannelPortsAdmin : UITest
    {
        [TestCategory("Users")]
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Logout();

            AssumeDate("01/07/2021");
            Goto("/");
            Set("Email").To("admin@uat.co");
            Set("Password").To("test");
            Click("Login");
            WaitToSeeHeader("Shipments");

        }
    }
}