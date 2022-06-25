using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewEADConsignmentLogsPage_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentForWWL>();

            //navigate
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/01/2020");
            Goto("/");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            WaitToSeeHeader("Shipments");
            AtRow(That.Contains, "Worldwide").Column("Tracking number").ClickLink();

            WaitToSeeHeader("Shipment Details");

            ExpectRow("T072100000101");
            ExpectColumn("Logs");
            AtRow("T072100000101").Column("Logs").ClickLink();

            ExpectHeader(That.Contains, "Logs");

            ExpectButton("Back");
        }
    }
}