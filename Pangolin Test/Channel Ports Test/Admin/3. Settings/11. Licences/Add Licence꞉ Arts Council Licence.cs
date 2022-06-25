using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddLicenceArtsCouncilLicence : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Licences");

            WaitToSeeHeader("Licences");

            Click("New Licence");

            WaitToSeeHeader("Licence Details");

            Set("Type").To("Into UK");
            Set("Licence name").To("Arts Council Licence");
            Set("Licence identifier").To("ArtsCncl");
            AtLabel("Licence Type").ClickLabel("Electronic");
            Set("Chief licence code").To("123456");
            AtLabel("Quantity").ClickLabel("Yes");
            AtLabel("RPTID").ClickLabel("Yes");

            Click("Save");

            WaitToSeeHeader("Licences");
            ExpectRow("Arts Council Licence");

            AtRow("Arts Council Licence").Column("Licence identifier").Expect("ArtsCncl");
        }
    }
}