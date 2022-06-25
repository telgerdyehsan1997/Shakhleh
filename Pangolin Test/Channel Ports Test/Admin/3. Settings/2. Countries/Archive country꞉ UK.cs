using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveCountryUK : UITest
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
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            ExpectRow("United Kingdom");

            // ----------------------------------------------

            // Archive - Cancel
            AtRow("United Kingdom").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            ClickButton("Cancel");
            ExpectRow("United Kingdom");

            // ----------------------------------------------

            // Archive - OK
            NearLabel("Archived").ClickLabel("Active");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Countries");
            AtRow("United Kingdom").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("United Kingdom");

            NearLabel("Active").ClickLabel("Archived");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Countries");
            ExpectRow("United Kingdom");
            AtRow("United Kingdom").ExpectNo("Archive");
            AtRow("United Kingdom").Expect("Unarchive");
        }
    }
}