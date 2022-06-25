using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCountry_UnitedKingdom : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            /*
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            Click("New Country");
            WaitToSeeHeader("Country Details");

            // ----------------------------------------------

            // Set United Kingdom details
            Set("Country").To("United Kingdom");
            Set("Country code").To("GB");
            ExpectNoField("Import CPC with preference");
            ExpectNoField("Export CPC with preference");
            AtLabel("Preference available").ClickLabel("Yes");
            ExpectField("Import CPC with preference");
            ExpectField("Export CPC with preference");
            Set("Import CPC with preference").To("1234567");
            Set("Import CPC without preference").To("0123456");
            Set("Export CPC with preference").To("1234567");
            Set("Export CPC without preference").To("0123456");
            Set("Additional code override with preference").To("VCL");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader("Countries");
            ExpectRow("United Kingdom");
            AtRow("United Kingdom").Column("Country code").Expect("GB");
            AtRow("United Kingdom").Column("Preference available").ExpectTick();
            AtRow("United Kingdom").Column("Import CPC with preference").Expect("1234567");
            AtRow("United Kingdom").Column("Import CPC without preference").Expect("0123456");
            AtRow("United Kingdom").Column("Export CPC with preference").Expect("1234567");
            AtRow("United Kingdom").Column("Export CPC without preference").Expect("0123456");
            AtRow("United Kingdom").Column("Additional code override with preference").Expect("VCL");
            */
        }
    }
}