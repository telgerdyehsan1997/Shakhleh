using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssignWorldwideLogisticsLtdAsDefaultDeclarant : UITest
    {
        [TestProperty("Sprint", "1")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyWorldwideLogisticsLtd>();

            LoginAs<ChannelPortsAdmin>();
            // ----------------------------------------------

            // Navigation
            ExpectHeader("Shipments");

            ClickLink("Companies");

            ExpectRow("Worldwide Logistics Ltd");

            AtRow(That.Contains, "Worldwide Logistics Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Worldwide Logistics Ltd");
            AtLabel(The.Top, "Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            ClickLink("Settings");
            ClickLink("Global Settings");

            // ----------------------------------------------

            ClickField("Default declarant");
            Type("Worldwide Logistics Ltd");
            System.Threading.Thread.Sleep(500);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click("Save");

            // ----------------------------------------------

            ClickLink("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Worldwide Logistics Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Worldwide Logistics Ltd");
            //AtLabel("Default declarant").Expect("Yes");
        }
    }
}