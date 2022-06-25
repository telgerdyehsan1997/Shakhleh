using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRouteSouthamptonAndValencia : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNon_UKPortValencia>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader("Routes");

            Click("New Route");

            Set("Uk port").To("Southampton");
            Set("Non-UK port").To("VALENCIA");
            //AtLabel("Manual").Click("No");
            ClickXPath(@"//*[@id=""IsManual""]/div[2]/label");

            Click("Save");

            ExpectHeader("Routes");

            AtRow("Southampton").Column("Uk port").Expect("Southampton");
            AtRow("Southampton").Column("Non-UK port").Expect("Valencia");
        }
    }
}