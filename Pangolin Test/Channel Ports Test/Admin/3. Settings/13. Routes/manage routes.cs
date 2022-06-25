using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageRoutes : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRouteBlackpoolAndCalais,ArchiveRouteSouthamptonAndValencia>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");



        }
    }
}
