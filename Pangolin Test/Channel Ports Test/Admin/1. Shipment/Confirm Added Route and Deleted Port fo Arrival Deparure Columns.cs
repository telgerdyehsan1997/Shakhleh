using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ConfirmAddedRouteAndDeletedPortfoArrivalDeparureColumns : UITest
    {
        [TestProperty("Sprint", "21")]
        [TestProperty("AMP", "142573")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentIntoUKASMAccepted>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            ExpectRow("R0721000001");

            ExpectColumn("Route");
            ExpectNoColumn("Port of arrival/departure");
            AtRow("R0721000001").Column("Route").Expect("AMSTERDAM to Portsmouth");
        }
    }
}
