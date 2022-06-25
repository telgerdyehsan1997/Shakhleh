using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewContactGroup_Import : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

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

            // Create contact
            Click("New Contact Group");
            Set("Group name").To("Import");
            Click("Save");

            // ----------------------------------------------

            ExpectRow("Import");
            AtRow("Import").Column("Contacts").Expect("0");
        }
    }
}