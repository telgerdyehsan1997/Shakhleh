using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveContactGroup_Import : UITest
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
            ClickLabel("All");

            // ----------------------------------------------

            // Edit Import - cancel
            AtRow("Import").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Click("Cancel");

            // ----------------------------------------------

            // Expect no changes
            AtRow("Import").Column("Archive").Click("Archive");

            // ----------------------------------------------

            // Edit Import - save
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");

            // ----------------------------------------------

            // Expect  changes
            WaitToSeeHeader("Contact Groups");
            AtRow("Import").Column("Archive").Expect("Unarchive");
        }
    }
}