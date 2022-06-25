using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewTransitOffice : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Offices of Transit");

            WaitToSeeHeader("Offices of Transit");

            Click("New Office of Transit");

            WaitToSeeHeader("Office of Transit details");

            Set("Country code").To("UK");
            Set("Country name").To("United Kingdom");
            Set("Usual name").To("Dover Office2");
            Click("Add Alias");
            Set("Alias").To("Dover");
            Set("NCTS Code").To("DO987654");
            AtLabel("Departure").ClickLabel("Yes");
            AtLabel("Destination").ClickLabel("Yes");
            AtLabel("Transit").ClickLabel("Yes");

            Click("Save");

            WaitToSeeHeader("Offices of Transit");
            ExpectRow("Dover Office2");
            AtRow("Dover Office2").Column("NCTS Code").Expect("DO987654");
            AtRow("Dover Office2").Column("Country Code").Expect("UK");
            AtRow("Dover Office2").Column("Country name").Expect("United Kingdom");

        }
    }
}