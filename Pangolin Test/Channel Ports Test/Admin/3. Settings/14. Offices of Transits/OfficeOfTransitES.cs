using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class OfficeOfTransitES : UITest
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

            Set("Country code").To("ES");
            Set("Country name").To("Spain");
            Set("Usual name").To("Madrid");
            Click("Add Alias");
            Set("Alias").To("Madrid");
            Set("NCTS Code").To("ES001111");
            AtLabel("Departure").ClickLabel("Yes");
            AtLabel("Transit").ClickLabel("Yes");
            AtLabel("Destination").ClickLabel("Yes");

            Click("Save");

            AtRow("MADRID").Column("NCTS Code").Expect("ES001111");
            AtRow("MADRID").Column("Country Code").Expect("ES");
            AtRow("MADRID").Column("Country name").Expect("Spain");
        }
    }
}