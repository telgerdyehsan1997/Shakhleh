using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewNCTSOnSitePage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS On Site
            ClickLink("Dashboard");

            ExpectHeader("Support Tickets");
            ClickLink("NCTS On Site");

            //Asserts Page labels and headers
            ExpectHeader("NCTS On Site");
            ExpectLabel("Status");
            AtLabel("Status").ExpectLabel("All");
            AtLabel("Status").ExpectLabel("Active");
            AtLabel("Status").ExpectLabel("Closed");
            ExpectButton("Search");
        }
    }
}