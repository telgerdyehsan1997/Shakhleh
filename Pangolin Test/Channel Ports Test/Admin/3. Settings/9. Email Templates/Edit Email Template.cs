using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditEmailTemplate : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Email Templates");
            WaitToSeeHeader("Email Templates");

            Click(The.Bottom, "2");
            ExpectRow("Welcome Email");
            ExpectRow("Recover Password");
            ExpectRow("Shipment Submission");

            AtRow("Welcome Email").Column("Edit").Click("Edit");
            WaitToSeeHeader("Email Template Details");
            RightOf("Key").Expect("Welcome Email");
            RightOf("Mandatory placeholders").Expect("USERNAME, LINK");

            Set("Subject").To("Welcome to Channel Ports");
            Set("Body").To(@"Dear [#USERNAME#], Your account has been created successfully. Please click on the following link to set your password. Best regards Channel Ports");

            Click("Save");

            ExpectText(That.Contains, "Email template subject or body must have all place-holders for WelcomeEmail");
            Click("OK");
            Set("Body").To(@"Dear [#USERNAME#], Your account has been created successfully. Please click on the following link to set your password. [#LINK#]");

            Click("Save");

            ExpectRow("Welcome Email");
            AtRow("Welcome Email").Column("Subject").Expect("Welcome to Channel Ports");
            AtRow("Welcome Email").Column("Body").Expect(@"Dear [#USERNAME#], Your account has been created successfully. Please click on the following link to set your password. [#LINK#]");
            AtRow("Welcome Email").Column("Mandatory placeholders").Expect("USERNAME, LINK");

            ClickLink("Users");
            WaitToSeeHeader("Users");
            Click("New User");
            WaitToSeeHeader("User Details");

            Set("First name").To("Steve");
            Set("Last name").To("Harolds");
            Set("Email").To("steve@uat.co");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Save");

            WaitToSeeHeader("Users");
            ExpectRow("Steve");

            CheckMailBox("steve@uat.co");
            ExpectText("Welcome to Channel Ports");
            ClickText("Welcome to Channel Ports");
            WaitToSeeHeader(That.Contains, "Welcome to Channel Ports");
            ExpectText(That.Contains, "Dear Steve Harolds, Your account has been created successfully. Please click on the following link to set your password.");
        }
    }
}