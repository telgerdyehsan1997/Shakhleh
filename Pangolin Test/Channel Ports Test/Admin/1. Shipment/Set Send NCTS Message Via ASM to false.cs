using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetSendNCTSMessageViaASMToFalse : UITest
    {
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            Click("Settings");
            ExpectHeader("Users");
            Click("Global Settings");
            ExpectHeader("Global Settings");
            AtLabel("Send NCTS Message Via ASM").ClickLabel("No");
            AtLabel("Activate UCN").ClickLabel("No");
            Click("Save");
        }
    }
}