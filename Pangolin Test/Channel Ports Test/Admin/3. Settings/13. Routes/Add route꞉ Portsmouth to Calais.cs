using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRoutePortsmouthToCalais : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNon_UKPortCalais>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");

            Click("New Route");

            Set("Uk port").To("Portsmouth");
            Set("Non-UK port").To("CALAIS");
            AtLabel("Manual").ClickLabel("No");
            Click("Save");

            ExpectHeader(That.Contains, "Routes");

            AtRow("Portsmouth").Column("Uk port").Expect("Portsmouth");
            AtRow("Portsmouth").Column("Non-UK port").Expect("Calais");
        }
    }
}