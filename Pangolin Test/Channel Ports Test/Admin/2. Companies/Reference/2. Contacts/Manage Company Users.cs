using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCompanyUsers : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCompanyUser_JohnSmith>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Company Users");
            WaitToSeeHeader("Company Users");
            ExpectRow("John");

            // ----------------------------------------------

            // Search - inclusive
            Set("Find").To("Jack");
            Click("Search");
            ExpectNoRow("John");

            // ----------------------------------------------

            // Search - exclusive
            Set("Find").To("John");
            Click("Search");
            ExpectRow("John");

            // ----------------------------------------------

            // Archive contact
            AtRow(That.Contains, "John").Column(That.Contains, "Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");
            ExpectNoRow(That.Contains, "John");

            // ----------------------------------------------

            // Search archived
            ClickLabel("Archived");
            Click("Search");
            ExpectRow("John");

            // ----------------------------------------------

            // Search all
            Set("Find").To("");
            ClickLabel("All");
            Click("Search");
            ExpectRow("John");

            // ----------------------------------------------

            // Unarchive contact
            AtRow(That.Contains, "John").Column(That.Contains, "Archive").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive Reason");
            Click(The.Left, "Unarchive");

            ExpectRow(That.Contains, "John");

            ClickLabel("Active");
            ClickButton("Search");
            ExpectRow("JOHN");
        }
    }
}