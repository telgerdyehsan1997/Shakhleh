using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertHealthCertificateCodeValidation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Health Certificate";
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Global Settings
            this.NavigateToSettingsPage(settingsPage);

            ClickLink("New Health Certificate");

            //Asserts that a Health Certificate Code is required if a description is provided
            Set("Description").To("Test Description");
            ClickButton("Save");
            Expect("The Code field is required.");

            //Asserts that a Health Certificate Code can only accept three characters
            Set("Description").To("");
            Set("Code").To("1");
            ClickButton("Save");
            Expect("Code should be 3 characters.");

            //Asserts that Health Certificate Code can be saved without a Description
            Set("Code").To("150");
            ClickButton("Save");
            ExpectNo("The Description field is required");
        }
    }
}