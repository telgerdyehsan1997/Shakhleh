using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportCommodityCodes_EUQuotaPref : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");

            // ----------------------------------------------

            Click("New Import");
            WaitToSeeHeader("Upload Commodity Codes");
            Set("Choose file").To("Commodity_2022-04-26_beta.csv");
            System.Threading.Thread.Sleep(2000);
            Click("Save");

            // ----------------------------------------------

            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Commodity Code Import Service").Click("Execute");
            Goto("/");

            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            Expect("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Partial success");
        }
    }
}