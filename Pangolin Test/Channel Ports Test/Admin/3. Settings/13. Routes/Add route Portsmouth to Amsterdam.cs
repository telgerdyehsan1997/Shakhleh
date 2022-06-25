using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRoutePortsmouthToAmsterdam : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNon_UKPortAmsterdam>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");

            Click("New Route");

            Set("Uk port").To("Portsmouth");
            Set("Non-UK port").To("Amsterdam");
            AtLabel("Manual").ClickLabel("No");

            Click("Save");

            ExpectRow(That.Contains, "AMSTERDAM");
        }
    }
}