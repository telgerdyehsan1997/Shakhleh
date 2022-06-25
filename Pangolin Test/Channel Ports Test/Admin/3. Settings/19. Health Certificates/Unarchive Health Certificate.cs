using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveHealthCertificate : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Health Certificate";

            Run<ArchiveHealthCertificate>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Global Settings
            this.NavigateToSettingsPage(settingsPage);

            ExpectNoRow("850");

            //Finds the Archived Health Certificate
            AtLabel("Status").ClickLabel("Archived");
            ClickButton("Search");
            ExpectRow("850");

            //Archives the Health Certificate
            AtRow("850").Column("Archive").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive Reason");
            ClickButton(The.Left, "Unarchive");
            ExpectNoRow("850");

            //Asserts that the Health Certificate Code has been Unarchived
            AtLabel("Status").ClickLabel("Active");
            ClickButton("Search");
            ExpectRow("850");
        }
    }
}