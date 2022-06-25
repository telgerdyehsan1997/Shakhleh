using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetBannerMessage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");

            ExpectHeader("Users");

            //Navigates to Banner Message
            ClickLink("Banner Message");

            ExpectHeader("Banner Messages");

            //Creates the new Banner Message
            ClickLink(The.Right, "Banner Message");

            ExpectHeader("Banner Message Details");
            Set("Message").To("Banner Message");
            ClickButton("Save");

            //Assert that Banner Message has been saved
            ExpectHeader("Banner Messages");
        }
    }
}