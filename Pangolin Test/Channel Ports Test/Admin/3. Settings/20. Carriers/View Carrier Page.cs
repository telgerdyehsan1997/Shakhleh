using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewCarrierPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Carriers";

            LoginAs<ChannelPortsAdmin>();

            //Navigates to the Carriers Page
            this.NavigateToSettingsPage(settingsPage);

            //Asserts Fields and labels
            ExpectField("Carrier name");
            ExpectLabel("Status");
            AtLabel("Status").Expect("All");
            AtLabel("Status").Expect("Archived");
            AtLabel("Status").Expect("Active");

            //Assert Links and Buttons
            ExpectLink("New Carrier");
            ExpectButton("Search");
        }
    }
}