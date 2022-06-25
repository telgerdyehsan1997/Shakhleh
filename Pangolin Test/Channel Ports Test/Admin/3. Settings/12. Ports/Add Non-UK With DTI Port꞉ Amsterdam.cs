using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNon_UKWithDtiPortAmsterdam : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Ports";
            var dtiBadge = "ABC";

            Run<OfficeOfTransitES>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings page
            this.NavigateToSettingsPage(settingsPage);

            //Creates the new Port
            Click("New Port");
            ExpectHeader("Port Details");
            Set("Port name").To("Amsterdam");
            Set("Transport mode").To("1");
            Click(What.Contains, "---Select---");
            Expect(What.Contains, "Greece");
            Click(What.Contains, "Greece");
            Set("Port code").To("AMS");
            Set("NCTS code").To("ES MADRID");
            Click(What.Contains, "ES MADRID");
            AtLabel("Non-UK").ClickLabel("Yes");
            ClickLabel(The.Top, "GVMS");
            Set("Into UK Value").To("D");
            ClickLabel(The.Bottom, "GVMS");
            Set("Out of UK Value").To("A");
            //DTI Badge must only be letters and three characters
            Set("DTI Badge").To(dtiBadge);
            ClickButton("Save");

            WaitToSeeHeader("Ports");
            ExpectRow("Amsterdam");
            AtRow("Amsterdam").Column("Port code").Expect("AMS");
            AtRow("Amsterdam").Column("Non-UK").ExpectTick();
            AtRow("Amsterdam").Column("DTI Badge").Expect(dtiBadge);
        }
    }
}