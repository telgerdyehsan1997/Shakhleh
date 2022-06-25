using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssignDefaultDeclarantToAnotherCompany : UITest
    {
        [Ignore]
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();
            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            ExpectRow("Truckers Ltd");

            // Assert initial values
            AtRow(That.Contains, "Worldwide Logistics Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Worldwide Logistics Ltd");
            AtLabel("Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Truckers Ltd");
            AtLabel("Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            // ----------------------------------------------

            // Assign default declarant to another company
            Click("Settings");
            WaitForNewPage();
            Click("Global Settings");
            WaitToSeeHeader("Global Settings");
            Set("Default declarant").To("Truckers Ltd");
            Click(What.Contains, "Truckers Ltd");
            Click("Save");

            // ----------------------------------------------

            // Assert changes
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Worldwide Logistics Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Worldwide Logistics Ltd");
            AtLabel("Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Truckers Ltd");
            AtLabel("Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
        }
    }
}
