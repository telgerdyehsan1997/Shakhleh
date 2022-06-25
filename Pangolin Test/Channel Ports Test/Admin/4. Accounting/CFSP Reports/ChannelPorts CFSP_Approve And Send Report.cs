using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ChannelPortsCFSP_ApproveAndSendReport : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ChannelPortsCFSP_GenerateCFSPReport>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Accounting
            ClickLink("Accounting");

            ExpectHeader("VAT Rates");

            //Navigates to CFSP Reports
            ClickLink("CFSP Reports");

            ExpectHeader("CFSP Reports");
            ExpectRow(That.Contains, "03/08/2021");
            AtRow(That.Contains, "03/08/2021").Column("Approve").ClickButton("Approve");

            //Runs the background task to send the CFSP Report
            CheckBackgroundTasks();
            AtRow("Send CFSP monthly report").Column("Execute").Click("Execute");
            System.Threading.Thread.Sleep(1000);
            Goto("/");

            //Checks to see if the Report has been sent
            CheckMailBox("");
        }
    }
}