using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditRoute : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            // Run<AddPortPortsmouth>();
            Run<AddRouteSouthamptonAndValencia>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");

            AtRow("Southampton").Column("Edit").Click("Edit");

            Set("Uk port").To("Blackpool");
            Set("Non-UK port").To("VALENCIA");

            Click("Save");

            ExpectHeader(That.Contains, "Routes");

            AtRow("Blackpool").Column("Uk port").Expect("Blackpool");
            AtRow("Blackpool").Column("Non-UK port").Expect("Valencia");

            AtRow("Blackpool").Column("Edit").Click("Edit");
            AtLabel("Manual").ClickLabel("Yes");

            Click("Save");
        }
    }
}