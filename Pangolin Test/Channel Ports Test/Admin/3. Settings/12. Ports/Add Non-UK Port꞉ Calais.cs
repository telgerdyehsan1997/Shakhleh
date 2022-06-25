using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNon_UKPortCalais : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126987")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewOfficeOfTransit_France>();
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Ports");

            WaitToSeeHeader("Ports");

            Click("New Port");

            WaitToSeeHeader("Port Details");
            Set("Port name").To("Calais");
            Set("Transport mode").To("1");
            AtLabel("Country").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "France");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "France");
            Set("Port code").To("CAL");
            this.ClickAndWait("NCTS code", "FR FRANCE TRANSIT FRCALTRA FRANCE - FRCALTRA");
            AtLabel("Non-UK").ClickLabel("Yes");
            ClickLabel(The.Top, "GVMS");
            Set("Into UK Value").To("d");
            ClickLabel(The.Bottom, "GVMS");
            Set("Out of UK Value").To("a");
            Click("Save");

            WaitToSeeHeader("Ports");
            ExpectRow("Calais");
            AtRow("Calais").Column("Port code").Expect("CAL");
            AtRow("Calais").Column("Non-UK").ExpectTick();
        }
    }
}