using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UpdateCountryWithEU27Flag_France : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            ExpectRow("France");
            AtRow("France").Column("EU27").ExpectTick();

            // ----------------------------------------------

            AtRow("France").Column("Country").ClickLink();
            WaitToSeeHeader("Country Details");

            AtLabel("EU27").ClickLabel("No");

            Click("Save");

            AtRow("France").Column("EU27").ExpectNoTick();
        }
    }
}