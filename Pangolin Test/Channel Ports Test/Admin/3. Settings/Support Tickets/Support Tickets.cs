using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SupportTickets : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Support Tickets";

            Run<SetHealthCertificateCode>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Global Settings
            this.NavigateToSettingsPage(settingsPage);

            ExpectRow("850");

            //Archives the Health Certificate
            AtRow("850").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton(The.Left, "Archive");
            ExpectNoRow("850");

            //Asserts that the Health Certificate Code has been Archived
            AtLabel("Status").ClickLabel("Archived");
            ClickButton("Search");
            ExpectRow("850");
        }
    }
}