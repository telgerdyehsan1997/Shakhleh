using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentIntoUKWith2CommoditiesASMAcc : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentIntoUKWith2Commodities>();

            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            AssumeDate("06/01/2022");
            Goto("/");

            ExpectRow("R0122000001");

            //----transmit to ASM----

            AtRow("R0122000001").Column("Tracking number").Click("R0122000001");

            AtRow("R012200000101").Column("Transmit").Click("Transmit");
            ExpectRow("R012200000101");

            //AtRow("R012100000101").Column("Progress").Expect("ASMAccepted");


        }
    }
}