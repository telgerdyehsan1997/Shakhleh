using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNon_UKPortValencia : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126987")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Ports");

            WaitToSeeHeader("Ports");

            Click("New Port");

            WaitToSeeHeader("Port Details");
            Set("Port name").To("Valencia");
            AtLabel("Country").Click(What.Contains, "---Select---");
            Expect(What.Contains, "Italy");
            Click(What.Contains, "Italy");
            Set("Port code").To("VAL");
            Set("NCTS code").To("United Kingdom - GB000060");
            Click(What.Contains, "United Kingdom - GB000060");
            AtLabel("Non-UK").ClickLabel("Yes");
            ClickLabel(The.Top, "GVMS");
            Set("Into UK Value").To("1");
            Set("Out of UK type").To("Inventory");
            Set("Out of UK Value").To("2");
            Click("Save");

            WaitToSeeHeader("Ports");
            ExpectRow("Valencia");
            AtRow("Valencia").Column("Port code").Expect("VAL");
            AtRow("Valencia").Column("NCTS code").Expect("GB000060");
            AtRow("Valencia").Column("Non-UK").ExpectTick();
        }
    }
}