using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_EditChargeBand : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var valueBand = "£5001+";
            var charge = "£140.00";

            LoginAs<Undischarged_ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Charge Bands");
            WaitToSeeHeader("Charge Bands");

            ExpectRow(valueBand);

            AtRow(valueBand).Column("Edit").Click("Edit");
            WaitToSeeHeader($"[#{valueBand}#] Charge");

            Set("Charge").To(charge);
            Click("Save");

            AtRow(valueBand).Column("Channelports Administration Charge").Expect(charge);
        }
    }
}
