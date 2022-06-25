using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetNCTSHighValueThresholdTo300K : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Global Settings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Global Settings");
            ExpectHeader("Global Settings");

            //Sets High Value Threshold
            AtLabel("Activate UCN").ClickLabel("No");
            Set("NCTS High Value Threshold").To("300000");
            ClickButton("Save");
        }
    }
}