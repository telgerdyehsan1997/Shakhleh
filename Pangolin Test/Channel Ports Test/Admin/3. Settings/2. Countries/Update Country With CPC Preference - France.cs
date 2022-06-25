using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [Ignore]
    [TestClass]
    public class UpdateCountryWithCPCPreference_France : UITest
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

            // ----------------------------------------------

            AtRow("France").Column("Country").ClickLink();
            WaitToSeeHeader("Country Details");
            ClickLabel("Preference available");

            ExpectNoField("Import CPC with preference");
            ExpectNoField("Import CPC with preference declaration type");
            ExpectNoField("Import CPC with preference preference code");
            ExpectNoField("Export CPC with preference");
            ExpectNoField("Export CPC with preference declaration type");
            ClickLabel("Preference available");
            ExpectField("Import CPC with preference");
            ExpectField("Import CPC with preference declaration type");
            ExpectField("Import CPC with preference preference code");
            ExpectField("Export CPC with preference");
            ExpectField("Export CPC with preference declaration type");

            ClickField("Import CPC with preference");
            Type("CP54321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CP54321");

            Set("Import CPC with preference declaration type").To("DE");
            Set("Import CPC with preference preference code").To("567");

            ClickField("Export CPC with preference");
            Type("CP12345");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CP12345");

            Set("Export CPC with preference declaration type").To("GH");

            Click("Save");

            AtRow("France").Column("Preference available").ExpectTick();
            AtRow("France").Column("Import CPC with preference").Expect("CP54321");
            AtRow("France").Column("Import CPC with preference declaration type").Expect("DE");
            AtRow("France").Column("Import CPC with preference preference code").Expect("567");
            AtRow("France").Column("Export CPC with preference").Expect("CP12345");
            AtRow("France").Column("Export CPC with preference declaration type").Expect("GH");
        }
    }
}