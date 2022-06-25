using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewEADConsignmentLogsPage_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentToTruckersLtd>();

            //navigate
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/01/2020");
            Goto("/");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            WaitToSeeHeader("Shipments");
            AtRow(That.Contains, "R0721000001").Column("Tracking number").ClickLink();

            WaitToSeeHeader("Shipment Details");

            ExpectRow("R072100000101");
            ExpectColumn("Logs");
            AtRow("R072100000101").Column("Logs").ClickLink();

            ExpectHeader(That.Contains, "Logs");

            ExpectButton("Back");
        }
    }
}