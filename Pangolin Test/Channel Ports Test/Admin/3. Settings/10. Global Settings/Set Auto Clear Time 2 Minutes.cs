using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetAutoClearTime2Minutes : UITest
    {
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            Click("Settings");
            Click("Global Settings");
            ExpectHeader("Global Settings");

            Set("Time until Cleared").To("2");
            Click("Save");
            AtField("Time until Cleared").ExpectValue("2");
        }
    }
}