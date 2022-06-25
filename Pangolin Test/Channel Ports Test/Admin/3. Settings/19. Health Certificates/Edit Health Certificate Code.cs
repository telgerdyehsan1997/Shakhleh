using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditHealthCertificateCode : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Health Certificate";

            LoginAs<ChannelPortsAdmin>();
            Run<SetHealthCertificateCode>();

            //Navigates to Global Settings
            this.NavigateToSettingsPage(settingsPage);

            ExpectRow("850");

            //Edits the Health Certificate Code
            AtRow("850").Column("Edit").ClickLink();
            ExpectHeader("Health Certificate");
            Set("Code").To("704");
            Set("Description").To("Edited Description");
            ClickButton("Save");

            //Asserts that Health Certificate Code has been edited
            ExpectRow("704");
            ExpectNoRow("850");
            AtRow("704").Column("Description").Expect("Edited Description");
            AtRow("704").Column("Description").ExpectNo("Plant Products");
        }
    }
}