using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ChannelPortsCFSP_GenerateCFSPReport : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<TransmitCFSPChannelPortsShipment>();
            LoginAs<ChannelPortsAdmin>();

            //Assumes the date to be the 3rd of the following month
            AssumeDate("03/08/2021");
            AssumeTime("14:00");
            Goto("/");

            //Runs the background task to generate the CFSP Report
            CheckBackgroundTasks();
            AtRow("Create CFSP monthly report").Column("Execute").Click("Execute");
            System.Threading.Thread.Sleep(1000);
            Goto("/");

            //Navigates to Accounting
            ClickLink("Accounting");

            ExpectHeader("VAT Rates");

            //Navigates to CFSP Reports
            ClickLink("CFSP Reports");

            ExpectHeader("CFSP Reports");
            ExpectRow(That.Contains, "03/08/2021");
            AtRow(That.Contains, "03/08/2021").Column("Download").Click("Download");
            ExpectHeader("CFSP Reports");
        }
    }
}