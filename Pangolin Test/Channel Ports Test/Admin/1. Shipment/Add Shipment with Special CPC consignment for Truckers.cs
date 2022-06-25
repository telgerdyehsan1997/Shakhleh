using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentWithSpecialCPCConsignmentForTruckers : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<TryToCompleteShipmentWithNonMatchingCommodityValue>();

            LoginAs<ChannelPortsAdmin>();

            ClickLink("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Bottom, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow("T0721000001").Column("Consignments").Click("1");
            AtRow("T072100000101").Click("1");
            Click("Transmit");

            Expect("The total value for consignment is £2000.00 and the total value for the commodities within this consignment is £3999.99. These values do not match.");
            Click("OK");
        }
    }
}