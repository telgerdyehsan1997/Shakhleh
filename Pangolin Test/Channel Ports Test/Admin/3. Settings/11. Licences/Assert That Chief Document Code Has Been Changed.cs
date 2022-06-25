using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatChiefDocumentCodeHasBeenChanged : UITest
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

            //Views the 'Licence Details' page
            ClickLink("New Licence");

            //Assert that 'Chief Document Code' has been changed
            ExpectHeader("Licence Details");
            ExpectNo("Chief document code");
            Expect("Licence status code");
        }
    }
}