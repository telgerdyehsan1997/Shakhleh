using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GeeksQAUser : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Logout();

            Goto("/");
            Set("Email").To("rafalqa@uat.co");
            Set("Password").To("test");
            Click("Login");
            //WaitToSeeHeader("Shipments");

        }
    }
}