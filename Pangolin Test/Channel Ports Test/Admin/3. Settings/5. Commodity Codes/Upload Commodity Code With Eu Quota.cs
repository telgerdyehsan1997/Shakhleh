using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UploadCommodityCodeWithEuQuota : UITest
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
            Set("Choose file").To("CommodityWithEuQuota.csv");
            Click("Save");
            System.Threading.Thread.Sleep(15000);

            // ----------------------------------------------

            WaitToSeeHeader("Commodity Code Imports");
            //AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Commodity Code Import Service").Click("Execute");
            Goto("/");

            Click("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");
            System.Threading.Thread.Sleep(5000);
            RefreshPage();
            AtRow("Download").Column("Import status").Expect("Successful");

            AboveLink("Commodity Code Imports").Click("Commodity Codes");
            WaitToSeeHeader("Commodity Codes");

            AtRow("13").Column("Commodity code (export)").Expect("1012100");
            AtRow("1012100").Column("Commodity code (import)").Expect("13");
            //AtRow("01012100").Column("Second quantity").Expect("");
            //AtRow("01012100").Column("Third quantity").Expect("");
            AtRow("1012100").Column("VAT").Expect("S");
            AtRow("1012100").Column("WTO full rate").Expect("0.0%");
            AtRow("1012100").Column("EU Quota").Expect("123456");
        }
    }
}