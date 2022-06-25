using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddPortGoole : UITest
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
            AtLabel("Country").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "United Kingdom");
            Set("Port name").To("Goole");
            Set("Port code").To("Goo");
            ClickField("NCTS code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM - GB000060");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM - GB000060");
            AtLabel("Non-UK").ClickLabel("No");
            Set("UKBF email").To("go@uat.co");

            AtLabel("Into UK Type").ClickLabel("GVMS");
            Set("Into UK Value").To("D");
            AtLabel("Out Of UK Type").ClickLabel("Inventory");
            Set("Out Of UK Value").To("C");

            Click("Save");

            WaitToSeeHeader("Ports");
            ExpectRow("Goole");
            AtRow("Goole").Column("Port code").Expect("Goo");
        }
    }
}