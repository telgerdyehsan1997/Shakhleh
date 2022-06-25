using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddLicenceStatusCode_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");
            ExpectHeader("Users");

            //Navigates to Licences
            ClickLink("Licences");
            ExpectHeader("Licences");

            //Adds the Licence Status Code
            ClickLink("New Licence Status Code");
            ExpectHeader("Status Code Cetails");
            Set("Status Code").To("INTO");
            AtLabel("Type").ClickLabel("Into uk");
            AtLabel("Licence Type").ClickLabel("Electronic");
            Set("Description").To("Into the UK Status Code");
            ClickButton("Save");

            //Assert that the Status Code has been saved
            ExpectHeader("Licences");
            ExpectRow("INTO");
        }
    }
}