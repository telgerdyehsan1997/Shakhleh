using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminNavigatesToNewDepositsPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Deposits page
            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow("Channel Ports").Column("Company name").ClickLink();
            ExpectHeader("Channel Ports");

            //Error will occur after clicking Deposits
            ClickLink("Deposits");

        }
    }
}