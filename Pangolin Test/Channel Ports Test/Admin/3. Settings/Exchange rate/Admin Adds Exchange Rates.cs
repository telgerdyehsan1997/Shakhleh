using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsExchangeRates : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Exchange Rates";

            LoginAs<ChannelPortsAdmin>();

            //Runs the background task to pull the Exchange Rates from HMRC
            this.GetExchangeRates();

            //Navigates to Exchange Rates
            this.NavigateToSettingsPage(settingsPage);

            //Asserts that Exchange Rates have been pulled from HMRC
            ExpectRow(That.Contains, "exrates-monthly-");
            AtRow(That.Contains, "exrates-monthly-").Column("URL").Expect(What.Contains, "http://www.hmrc.gov.uk/softwaredevelopers/rates/exrates-monthly");
        }
    }
}