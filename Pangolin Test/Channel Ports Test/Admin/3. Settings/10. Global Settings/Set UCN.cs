using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetUCN : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            Click("Settings");
            Click("Global Settings");
            ExpectHeader("Global Settings");

            AtLabel("Activate UCN").ClickLabel("Yes");
            Click("Save");
            ExpectHeader("Global Settings");
        }
    }
}