using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNon_UKPortAmsterdam : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126987")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<OfficeOfTransitES>();
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Ports");

            WaitToSeeHeader("Ports");

            Click("New Port");

            WaitToSeeHeader("Port Details");
            Set("Port name").To("Amsterdam");
            Set("Transport mode").To("1");
            Click(What.Contains, "---Select---");
            Expect(What.Contains, "Greece");
            Click(What.Contains, "Greece");
            Set("Port code").To("AMS");
            ClickField("NCTS code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ES MADRID");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES MADRID");
            AtLabel("Non-UK").ClickLabel("Yes");
            ClickLabel(The.Top, "GVMS");
            Set("Into UK Value").To("D");
            ClickLabel(The.Bottom, "GVMS");
            Set("Out of UK Value").To("A");
            Click("Save");

            ExpectRow("Amsterdam");
            AtRow("Amsterdam").Column("Port code").Expect("AMS");
            AtRow("Amsterdam").Column("Non-UK").ExpectTick();
        }
    }
}