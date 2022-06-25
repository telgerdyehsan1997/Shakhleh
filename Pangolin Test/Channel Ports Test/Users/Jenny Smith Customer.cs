using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithCustomer : UITest
    {
        [TestCategory("Users")]
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Logout();
            AssumeDate("01/07/2019");

            Goto("/");
            Set("Email").To("jennysmith@uat.co");
            Set("Password").To("test");
            Click("Login");

            WaitForNewPage();
        }
    }
}