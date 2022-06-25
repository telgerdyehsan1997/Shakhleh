using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditContactGroup_Import : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactGroup_Import>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader("Truckers Ltd");
            Click("Contact Groups");
            WaitToSeeHeader("Contact Groups");

            // ----------------------------------------------

            // Edit Import - cancel
            AtRow("Import").Column("Edit").Click("Edit");
            WaitToSeeHeader("Contact Group Details");
            Set("Group name").To("Edited");
            Click("Cancel");

            // ----------------------------------------------

            // Expect no changes
            WaitToSeeHeader("Contact Groups");
            ExpectNoRow("Edited");

            // ----------------------------------------------

            // Edit Import - save
            AtRow("Import").Column("Edit").Click("Edit");
            WaitToSeeHeader("Contact Group Details");
            Set("Group name").To("Edited");
            Click("Save");

            // ----------------------------------------------

            // Expect changes
            WaitToSeeHeader("Contact Groups");
            ExpectRow("Edited");
        }
    }
}