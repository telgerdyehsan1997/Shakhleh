using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class LoginPage : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //check layout
            Goto("/");
            ExpectHeader(That.Contains, "Please Login");
            BelowHeader(That.Contains, "Please Login").Expect(What.Contains, "Email");
            Below(The.Top, What.Contains, "Email").Above(What.Contains, "Forgot password").Expect(What.Contains, "Password");
            Below(The.Top, What.Contains, "Password").ExpectLink("Forgot password");
            NearLink("Forgot password").ExpectButton(That.Contains, "Login");

            // ----------------------------------------------

            //try logging in blank email/pw
            Click("Login");
            Expect(What.Contains, "The Password field is required.");
            Expect(What.Contains, "The Email field is required.");

            // ----------------------------------------------

            //incorrect username
            Set("Email").To("wrong@uat.co");
            Set("Password").To("test");
            Click("Login");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "The email or password was incorrect");
            System.Threading.Thread.Sleep(1000);
            Click("OK");

            // ----------------------------------------------

            //incorrect password
            Set("Email").To("admin@uat.co");
            Set("Password").To("wrong");
            Click("Login");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "The email or password was incorrect");
            System.Threading.Thread.Sleep(1000);
            Click("OK");

            // ----------------------------------------------

            //successful login
            Set("Email").To("admin@uat.co");
            Set("Password").To("test");
            Click("Login");
            ExpectHeader("Shipments"); //THIS WILL NEED UPDATING TO PRE-ADVICE ONCE DEVELOPED
        }
    }
}