using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DefaultDeclarantCannotBeAnArchivedCompany : UITest
    {
        [TestProperty("Sprint", "1")]
        [TestProperty("AMP", "113085")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyTruckersLtd, AdminAddsCompanyWorldwideLogisticsLtd>();
            LoginAs<ChannelPortsAdmin>();
            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");

            // ----------------------------------------------

            // Archive company - Assigned as default declarant
            AtRow("Worldwide Logistics Ltd").Click("Archive");
            Expect("Are you sure you want to archive this company?");
            Click("Ok");
            Expect("You cannot set an archived Company as Default declarant.");
            Click("Ok");
            ExpectRow("Worldwide Logistics Ltd");

            // Archive company - Default declarant assigned to another active company
            Click("Settings");
            WaitForNewPage();
            Click("Global Settings");
            WaitToSeeHeader("Global Settings");
            Set("Default declarant").To("Truckers Ltd");
            Click(What.Contains, "Truckers Ltd");
            Click("Save");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Click("Archive");
            Expect("Are you sure you want to archive this company?");
            Click("Ok");
            ExpectNoRow("Worldwide Logistics Ltd");
            ClickLabel(The.Top, "Archived");
            Click("Search");
            ExpectRow("Worldwide Logistics Ltd");

            // Edit archived company - assign as default declarant
            Click("Settings");
            WaitForNewPage();
            Click("Global Settings");
            WaitToSeeHeader("Global Settings");
            Set("Default declarant").To("Worldwide Logistics Ltd");
            ExpectNo("Worldwide Logistics Ltd");
        }
    }
}
