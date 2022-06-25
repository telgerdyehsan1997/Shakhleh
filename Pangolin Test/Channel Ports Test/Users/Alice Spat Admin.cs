using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AliceSpatAdmin : UITest
    {
        [TestCategory("Users")]
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Logout();
            AssumeDate("01/07/2019");

            Goto("/");
            Set("Email").To("aspat@uat.co");
            Set("Password").To("test");
            Click("Login");

            WaitForNewPage();
        }
    }
}