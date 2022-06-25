using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class GlobalSettingSave : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<Undischarged_ChannelPortsAdmin>();

            ClickLink("Settings");
            Click("Global Settings");
            ExpectHeader("Global Settings");

            Set("Days before reminder email is sent").To("10");
            Set("Maximum number of reminder Emails").To("20");
            Set("Delay between creating the record and sending stage 1 email").To("30");

            Click("Save");
            ExpectHeader("Global Settings");
        }
    }
}