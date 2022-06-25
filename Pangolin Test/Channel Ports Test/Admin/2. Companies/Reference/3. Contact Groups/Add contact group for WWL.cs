using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddContactGroupForWWL : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd,AddCompanyUserForWWLJenny>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ClickLabel("All");
            Click("Search");
            ExpectRow(That.Contains, "Worldwide Logistics Ltd");
            AtRow(That.Contains, "Worldwide Logistics Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader("Worldwide Logistics Ltd");
            Click("Contact Groups");
            WaitToSeeHeader("Contact Groups");

            // ----------------------------------------------

            // Create contact
            Click("New Contact Group");
            Set("Group name").To("WWL Group");
            Click("Save");

            // ----------------------------------------------

            ExpectRow("WWL Group");
            AtRow("WWL Group").Column("Contacts").Expect("0");
            AtRow("WWL Group").Click("0");
            ClickCheckbox(The.Top);
            Click("Save");
            Click("Contact Groups");
            AtRow("WWL Group").Column("Contacts").Expect("1");

        }
    }
}
