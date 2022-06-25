using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCurrencies : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCurrency_JPY, ArchiveCurrencyAUD>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Currencies");
            Click("Currencies");
            WaitToSeeHeader("Currencies");

            // ----------------------------------------------

            // Edit - Cancel
            AtRow("JPY").Click("Edit");
            WaitToSeeHeader("Currency Details");
            Set("Currency").To("HKD");
            Click("Cancel");
            ExpectNoRow("HKD");
            ExpectRow("JPY");

            // ----------------------------------------------

            // Edit - Save
            AtRow("JPY").Click("Edit");
            WaitToSeeHeader("Currency Details");
            Set("Currency").To("HKD");
            Click("Save");
            ExpectNoRow("JPY");
            ExpectRow("HKD");

            // ----------------------------------------------

            // Search status: archived
            Set("Status").To("Archived");
            Click("Search");
            ExpectRow("AUD");
            ExpectNoRow("JPY");

            // ----------------------------------------------

            // Search status: active
            ClickLabel("Active");
            Click("Search");
            ExpectNoRow("AUD");
            ExpectRow("HKD");

            // ----------------------------------------------

            // Search status: all
            Set("Status").To("All");
            Click("Search");
            ExpectRow("AUD");
            ExpectRow("HKD");

            // ----------------------------------------------

            // check currency must be 3 letter code
            Click("New Currency");
            Set("Currency").To("FRAN");
            Click("Save");
            ExpectNoRow("FRAN");
            ExpectRow("FRA");
        }
    }
}
