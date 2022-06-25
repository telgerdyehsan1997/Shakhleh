using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddPortGoole2 : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126987")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddPortGoole>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Settings");

            Click("Ports");

            WaitToSeeHeader("Ports");

            Click("New Port");

            WaitToSeeHeader("Port Details");
            Set("Port name").To("Goole2");
            AtLabel("Country").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "United Kingdom");
            Set("Port code").To("Goo");
            Set("NCTS code").To("United Kingdom - GB000060");
            Click(What.Contains, "United Kingdom - GB000060");
            AtLabel("Non-UK").ClickLabel("No");
            Set("UKBF email").To("go@uat.co");

            AtLabel("Into UK Type").ClickLabel("GVMS");
            Set("Into UK Value").To("A");
            AtLabel("Out Of UK Type").ClickLabel("Inventory");
            Set("Out Of UK Value").To("B");

            Click("Save");

            WaitToSeeHeader("Ports");
            ExpectRow("Goole");
            AtRow("Goole2").Column("Port code").Expect("Goo");
        }
    }
}