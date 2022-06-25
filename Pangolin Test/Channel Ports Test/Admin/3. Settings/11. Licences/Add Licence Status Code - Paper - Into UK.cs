using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddLicenceStatusCode_Paper_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");

            ExpectHeader("Users");

            //Navigate to Licences
            ClickLink("Licences");

            ExpectHeader("Licences");

            //Creates the new Licence
            ClickLink("New Licence Status Code");

            ExpectHeader("Status Code Cetails");
            Set("Status Code").To("Paper");
            AtLabel("Type").ClickLabel("Into uk");
            AtLabel("Licence Type").ClickLabel("Paper");
            Set("Description").To("Paper Description");
            ClickButton("Save");

            //Assert that Licence Status Code has been saved
            ExpectHeader("Licence Status Codes");       
        }
    }
}