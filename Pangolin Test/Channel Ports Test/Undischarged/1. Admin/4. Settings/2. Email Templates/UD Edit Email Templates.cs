using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_EditEmailTemplate : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<Undischarged_ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Email Templates");
            WaitToSeeHeader("Email Templates");

            ExpectRow("Forgot Password");

            AtRow("Forgot Password").Column("Edit").Click("Edit");
            WaitToSeeHeader("Email Template Details");
            RightOf("Key").Expect("Forgot Password");
            RightOf("Mandatory placeholders").Expect("RESETPASSWORDLINK");

            Set("Subject").To("Welcome to Undischarged Channel Ports");

            Set("Body").To(@"Dear, Your account has been created successfully. Please click on the following link to set your password. Best regards Channel Ports");

            Click("Save");

            Expect(What.Contains, "Email template subject or body must have all place-holders for ForgotPassword");
            Click("OK");
            Set("Body").To(@"Dear, Your account has been created successfully. Please click on the following link to set your password. [#RESETPASSWORDLINK#]");

            Click("Save");

            ExpectRow("Forgot Password");
            AtRow("Forgot Password").Column("Subject").Expect("Welcome to Undischarged Channel Ports");
            AtRow("Forgot Password").Column("Mandatory placeholders").Expect("RESETPASSWORDLINK");
        }
    }
}
