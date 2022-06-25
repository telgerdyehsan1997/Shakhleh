using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UploadCommodityCode : UITest
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
            Set("Choose file").To("Commodity.csv");
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
            AtRow("10011100").Column("Commodity code (import)").Expect("0");
            //AtRow("01012100").Column("Second quantity").Expect("");
            //AtRow("01012100").Column("Third quantity").Expect("");
            AtRow("10011100").Column("VAT").Expect("Z");
            AtRow("10011100").Column("WTO full rate").Expect("0.0%");

            // NOTE: Pangolin might fail from this point (possible timeout due to number of columns and data amount processed?).
            // If so, check the remaining parts manually.
            Set("Find").To("10061030");
            Click("Search");

            ExpectRow("10061030");

            Set("Find").To("99999999999");
            Click("Search");

            ExpectNoRow("10011100");
            ExpectNoRow("99999999999");
        }
    }
}