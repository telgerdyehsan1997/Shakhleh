using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class UDLoginPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Goto("/");

            //Asserts layout
            ExpectHeader("Please Login");
            ExpectField("Email");
            ExpectField("Password");
            ExpectButton("Login");
            ExpectLink("Forgot password");

            //Assert validation
            ClickButton("Login");
            Expect("The email field is required.");
            Expect("The Password field is required.");
            System.Threading.Thread.Sleep(1000);

            //Incorrect Email
            Set("Email").To("incorrect@uat.co");
            Set("Password").To("Wrong");
            ClickButton("Login");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "The email or password was incorrect");
            System.Threading.Thread.Sleep(1000);
            ClickButton("OK");

            //Incorrect Password
            Set("Email").To("undischargedadmin@uat.co");
            Set("Password").To("Wrong");
            ClickButton("Login");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "The email or password was incorrect");
            System.Threading.Thread.Sleep(1000);
            ClickButton("OK");

            //Correct Login
            Set("undischargedadmin@uat.co");
            Set("Password").To("test");
            ClickButton("Login");
            ExpectLink("Undischarged NCTS");
        }
    }
}