using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveCurrencyAUD : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCurrencyAUD>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Currencies");
            Click("Currencies");
            WaitToSeeHeader("Currencies");
            ClickLabel("Archived");
            Click("Search");
            ExpectRow("AUD");
            AtRow("AUD").Expect("Unarchive");

            // ----------------------------------------------

            // Unarchive - Cancel
            AtRow("AUD").Column("Archive").ClickLink();
            ExpectHeader("Unarchive");
            Click("Cancel");
            ExpectRow("AUD");

            // ----------------------------------------------

            // Unarchive - OK
            AtRow("AUD").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive reason");
            ClickButton("Unarchive");
            ExpectNoRow("AUD");

            ClickLabel("Active");
            Click("Search");
            ExpectRow("AUD");
            AtRow("AUD").ExpectNo("Unarchive");
        }
    }
}