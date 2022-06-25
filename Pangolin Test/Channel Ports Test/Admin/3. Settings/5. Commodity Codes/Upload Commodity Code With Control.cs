using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UploadCommodityCodeWithControl : UITest
    {
        [TestProperty("Sprint", "1")]

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
            Set("Choose file").To("CommoditiesAllWithControl.csv");
            System.Threading.Thread.Sleep(2000);
            Click("Save");

            // ----------------------------------------------

            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Commodity Code Import Service").Click("Execute");
            Goto("/");

            Click("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Processing");
            System.Threading.Thread.Sleep(65000);
            RefreshPage();
            AtRow("Download").Column("Import status").Expect("Successful");


            AboveLink("Commodity Code Imports").Click("Commodity Codes");
            WaitToSeeHeader("Commodity Codes");

            AtRow("10011100").Column("Commodity code (export)").Expect("10011100");
            AtRow("10011100").Column("Control").ExpectTick();
        }
    }
}