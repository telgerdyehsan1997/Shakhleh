using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_ChannelPortsAdmin : UITest
    {
        [TestCategory("Users")]
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Logout();

            AssumeDate("01/07/2021");
            Goto("/");
            Set("Email").To("undischargedadmin@uat.co");
            Set("Password").To("test");
            Click("Login");
            ExpectLink("Undischarged NCTS");
        }
    }
}