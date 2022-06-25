using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCurrency_JPY : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            ClickLink("Settings");
            WaitForNewPage();
            Expect("Currencies");
            Click("Currencies");
            WaitToSeeHeader("Currencies");
            Click("New Currency");
            WaitToSeeHeader("Currency Details");

            // ----------------------------------------------

            // Set GBP details
            Set("Currency").To("JPY");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader("Currencies");
            ExpectRow("JPY");
        }
    }
}