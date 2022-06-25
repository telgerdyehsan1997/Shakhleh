using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ICSSystem_GenerateURL : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            //Navigates to Settings
            ClickLink("Settings");
            ExpectHeader("Users");

            //Navigates to the ICS System
            ClickLink("ICS System Code");
            ExpectHeader("ICS System Code");

            //Generates the URL
            ClickLink("Generate");
            AtLabel("URL").Expect(What.Contains, "test-www.taxService.gov.uk");
        }
    }
}