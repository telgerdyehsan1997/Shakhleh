using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pangolin;

namespace Channel_Ports_Test.ErrorReports
{
    [TestClass]
    public class DeleteErrorReports : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<TransmitShipmentForTruckersLTD>();
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");

            Click("Settings");
            WaitToSeeHeader("Users");

            Click("ASM File Error Log");
            WaitToSeeHeader("Error Logs");

            //ClickCheckbox("select / deselect all");

            //Click("Resolve All");
        }
    }
}
