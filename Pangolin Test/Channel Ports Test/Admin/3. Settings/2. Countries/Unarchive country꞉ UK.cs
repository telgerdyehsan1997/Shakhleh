using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveCountryUK : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCountryUK>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            Click("Settings");
            WaitForNewPage();
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            ClickLabel("Archived");
            Click("Search");
            ExpectRow("United Kingdom");
            AtRow("United Kingdom").Expect("Unarchive");

            // ----------------------------------------------

            // Unarchive - Cancel
            AtRow("United Kingdom").Click("Unarchive");
            ExpectHeader("Unarchive");
            AtLabel("Archive Reason").Expect("ARCHIVE REASON");
            AtLabel("Logged in user").Expect("Geeks Admin");
            //AtLabel("IP").Expect("");
            //AtLabel("Date and time").Expect("01/07/2021 12:26:35");
            ClickButton("Cancel");
            ExpectRow("United Kingdom");

            // ----------------------------------------------

            // Unarchive - OK
            AtRow("United Kingdom").Click("Unarchive");
            ExpectHeader("Unarchive");
            AtLabel("Archive Reason").Expect("ARCHIVE REASON");
            AtLabel("Logged in user").Expect("Geeks Admin");
            //AtLabel("IP").Expect("");
            //AtLabel("Date and time").Expect("01/07/2021 12:26:35");
            ClickButton("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive Reason");
            ClickButton("Unarchive");
            ExpectNoRow("United Kingdom");
            ClickLabel("Active");
            Click("Search");
            ExpectRow("United Kingdom");
            AtRow("United Kingdom").ExpectNo("Unarchive");
            AtRow("United Kingdom").Expect("Archive");
        }
    }
}