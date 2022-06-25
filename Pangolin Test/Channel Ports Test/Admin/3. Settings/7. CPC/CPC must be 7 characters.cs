using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CPCMustBe7Characters : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");
            Click("CPC");
            WaitToSeeHeader("CPC");

            // try 6 characters
            Click("New CPC");
            Set("CPC number").To("123456");
            Click("Save");
            Expect("CPC Number should be 7 characters.");

            // try 8 characters
            Click("CPC");
            WaitToSeeHeader("CPC");
            Click("New CPC");
            Set("CPC number").To("1234567");
            Click("Save");
            Expect("The CPC description field is required.");
        }
    }
}