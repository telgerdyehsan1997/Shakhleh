using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class GlobalSettingCancel : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<GlobalSettingCancel>();
            LoginAs<Undischarged_ChannelPortsAdmin>();
            ClickLink("Settings");
            Click("Global Settings");
            ExpectHeader("Global Settings");

            Set("Days before reminder email is sent").To("20");
            Set("Maximum number of reminder Emails").To("20");
            Set("Delay between creating the record and sending stage 1 email").To("30");

            Click("Cancel");

            AtField("Days before reminder email is sent").Expect("10");
            AtField("Maximum number of reminder Emails").Expect("20");
            AtField("Delay between creating the record and sending stage 1 email").Expect("30");
        }
    }
}