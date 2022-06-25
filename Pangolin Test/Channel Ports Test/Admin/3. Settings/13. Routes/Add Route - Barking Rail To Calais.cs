using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRoute_BarkingRailToCalais : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddUKPort_BarkingRailTerminal_Inventory, AddNon_UKPortCalais>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");

            Click("New Route");

            Set("Uk port").To("BARKING RAIL TERMINAL");
            Set("Non-UK port").To("CALAIS");
            AtLabel("Manual").ClickLabel("No");

            Click("Save");

            ExpectRow(That.Contains, "BARKING RAIL TERMINAL");
        }
    }
}