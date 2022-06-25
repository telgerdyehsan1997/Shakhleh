using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSASMACCEmailCheck : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<NCTSASMAcceptedPrep>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date Created").To("28/06/2021");
            Set(The.Top, "to").To("04/07/2021");
            ClickButton("Search");

            AtRow("1000000").Column("Tracking number").Click("1000000");

            AtRow("CP100000001").Column("Transmit to HMRC").Click("Transmit");
        }
    }
}