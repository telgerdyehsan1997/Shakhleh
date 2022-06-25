using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CannotUploadOldCommodityCodeImportVersion : UITest
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
            Set("Choose file").To("Commodity (old template).csv");
            Click("Save");

            // ----------------------------------------------

            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Commodity Code Import Service").ClickLink();
            Goto("/");

            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Failed");

            AtRow("Download").Column("Errors").ClickLink();
            WaitToSeeHeader("Import Errors");

            Expect(What.Contains, "The following columns are missing:");
        }
    }
}