using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewTransitOfficePL : UITest
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

            Set("Country code").To("PL");
            Set("Country name").To("Poland");
            Set("Usual name").To("Szczecin");
            Click("Add Alias");
            Set("Alias").To("Dover");
            Set("NCTS Code").To("PL987654");
            AtLabel("Departure").ClickLabel("Yes");
            AtLabel("Transit").ClickLabel("Yes");
            AtLabel("Destination").ClickLabel("Yes");

            Click("Save");

            AtRow("Szczecin").Column("NCTS Code").Expect("PL987654");
            AtRow("Szczecin").Column("Country Code").Expect("PL");
            AtRow("Szczecin").Column("Country name").Expect("Poland");

        }
    }
}