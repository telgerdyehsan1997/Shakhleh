using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportRoWQuotas : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<RoW_ImportCommodityCodes>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");

            ExpectHeader("Users");

            //Navigates to RoW Imports
            ClickLink("Row Quotas Imports");

            ExpectHeader("ROW Quota Imports");

            //Imports the List of RoW Quotas
            ClickLink("New Import");
            ExpectHeader("Import");
            Set("Choose file").To("QuotaFromMeasures_2022-06-21.csv");
            System.Threading.Thread.Sleep(2000);
            Click("Save");
            ExpectHeader("ROW Quota Imports");
            AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Row Quota Import Service").Click("Execute");
            Goto("/");

            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Row Quotas Imports");

            ExpectHeader("ROW Quota Imports");
            ExpectRow("Successful");
        }
    }
}