using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompleteConsignmentsForTruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCommodityToAConsignment>();
            LoginAs<ChannelPortsAdmin>();

            WaitToSeeHeader("Shipments");
            Set("Date created").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");

            AtRow("R0721000001").Column("Consignments").ClickLink("");
            AtRow("R072100000101").Column("Progress").Expect("Ready to Transmit");
        }
    }
}