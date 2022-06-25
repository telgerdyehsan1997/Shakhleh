using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminRunsBackgroundTaskToDeleteEmailsOlderThen7DaysOld : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOut_CommodityValueOver300000>();
            LoginAs<ChannelPortsAdmin>();

            //Checks current Emails in the System
            CheckMailBox("");

            //Assumes Date to be 7 days ahead
            AssumeDate("12/07/2021");
            Goto("/");

            //Runs the Background task to delete Emails that are older than 7 days
            CheckBackgroundTasks();
            AtRow("Purge Email Item Queues Older Than 7 Days").Click("Execute");
            CheckMailBox("");
            ExpectNo("Archive Notification");
        }
    }
}