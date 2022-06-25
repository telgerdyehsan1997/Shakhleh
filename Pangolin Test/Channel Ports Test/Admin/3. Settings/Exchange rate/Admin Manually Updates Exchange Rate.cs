using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminManuallyUpdatesExchangeRate : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Exchange Rates";

            Run<AdminPullsExchangeRatesFromHMRC>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to settings page
            this.NavigateToSettingsPage(settingsPage);

            //Navigates to Rates
            AtRow(That.Contains, "exrates-monthly-").ClickLink("Rates");
            ExpectHeader(That.Contains, "Exchange rates exrates-monthly");

            //Updates the Rate for the first entry
            AtRow("Abu Dhabi").Column("Update").ClickLink("Update Rate");
            ExpectHeader("Update Rate");
            Set("Rate").To("7.777");
            ClickButton("Save");

            //Assert that the updated date is stored for Rates
            AtRow("Abu Dhabi").Column("Rate").Expect("7.7770");
            AtRow("Abu Dhabi").Column("Update").ClickLink("Update Rate");
            ExpectHeader("Update Rate");
            AtField("Rate").Expect("7.7770");
            AtLabel("Date Last Updated").Expect("01/07/2021");
        }
    }
}