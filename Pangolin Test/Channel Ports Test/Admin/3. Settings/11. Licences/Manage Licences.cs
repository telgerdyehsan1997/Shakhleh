using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageLicences : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddLicenceArtsCouncilLicence>();

            // ------------------------------
            LoginAs<ChannelPortsAdmin>();

            Click("Settings");

            Click("Licences");

            WaitToSeeHeader("Licences");

            // Add - Cancel
            Click("New Licence");
            WaitToSeeHeader("Licence Details");
            Click("Cancel");

            // Edit - Cancel
            AtRow("Arts Council Licence").Click("Edit");
            WaitToSeeHeader("Licence Details");
            AtLabel("Licence type").ClickLabel("Paper");
            Click("Cancel");

            ExpectNoRow("Standard Import Licence");
            ExpectRow("Arts Council Licence");

            // Edit - Save
            AtRow("Arts Council Licence").Click("Edit");
            WaitToSeeHeader("Licence Details");
            Set("Licence Name").To("Standard Import Licence");
            AtLabel("Licence type").ClickLabel("Paper");
            Click("Save");

            ExpectNoRow("Arts Council Licence");
            ExpectRow("Standard Import Licence");

            // Archive - Cancel
            AtRow("Standard Import Licence").Click("Archive");
            ExpectHeader("Archive");
            Click("Cancel");
            AtRow("Standard Import Licence").Column("Archive").Expect("Archive");

            // Archive - OK
            AtRow("Standard Import Licence").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");
            ExpectNoRow("Standard Import Licence");
            ClickLabel(The.Top, "All");
            Click(The.Top, "Search");
            ExpectRow("Standard Import Licence");
            AtRow("Standard Import Licence").Column("Archive").Expect("Unarchive");

            // Find
            Set("Find").To("Standard");
            Click(The.Top, "Search");
            ExpectRow("Standard Import Licence");

            Set("Find").To("");
            Click(The.Top, "Search");

            // Validation
            Click("New Licence");
            WaitToSeeHeader("Licence Details");
            Click("Save");

            Expect("The Licence type field is required.");
            Expect("The RPTID field is required.");
            Expect("The Licence identifier field is required.");
        }
    }
}