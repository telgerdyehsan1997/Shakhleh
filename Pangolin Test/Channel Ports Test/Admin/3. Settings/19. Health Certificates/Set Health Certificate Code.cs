using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetHealthCertificateCode : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Health Certificate";

            LoginAs<ChannelPortsAdmin>();

            //Navigates to Global Settings
            this.NavigateToSettingsPage(settingsPage);

            //Sets the Health Certificate Code
            ClickLink("New Health Certificate");
            Set("Code").To("850");
            Set("Description").To("Plant Products");
            ClickButton("Save");
            ExpectHeader("Health Certificate");
        }
    }
}