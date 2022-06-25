using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveCurrencyAUD : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCurrency_AUD>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Currencies");
            Click("Currencies");
            WaitToSeeHeader("Currencies");
            ExpectRow("AUD");

            // ----------------------------------------------

            // Archive - Cancel
            AtRow("AUD").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Click("Cancel");

            // ----------------------------------------------

            // Archive - OK
            NearLabel("Archived").ClickLabel("Active");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Currencies");
            AtRow("AUD").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archived Currency");
            ClickButton("Archive");
            WaitToSeeHeader(That.Contains, "Currencies");
            ExpectNoRow("AUD");

            NearLabel("Active").ClickLabel("Archived");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Currencies");
            ExpectRow("AUD");
            AtRow("AUD").Expect("Unarchive");
        }
    }
}