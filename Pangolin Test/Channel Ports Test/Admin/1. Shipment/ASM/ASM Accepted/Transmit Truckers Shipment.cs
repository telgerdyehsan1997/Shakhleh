using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitTruckersShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCommoditiesForTruckersLTDGBP>();
            LoginAs<ChannelPortsAdmin>();


        }
    }
}