using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CommodityCodeImportShouldNotUseArchivedSecondQuantityDescriptions : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<ArchiveSecondQuantityDescription_Litres,CreateNewSecondQuantityDescription_SquareMetres>();
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            Click("Second Quantity Descriptions");
            WaitToSeeHeader(That.Contains, "Second Quantity Descriptions");

            // Edit Active SQD
            ClickLabel("Active");
            Click("Search");

            ExpectRow("020");

            AtRow(That.Contains, "020").Column(That.Contains, "Edit").Click("Edit");
            Set("Quantity code").To("061");
            Click("Save");
            WaitToSeeHeader("Second Quantity Descriptions");

            ExpectNoRow(That.Contains, "020");
            ExpectRow(That.Contains, "061");

            // Edit archived SQD
            ClickLabel("Archived");
            Click("Search");

            ExpectRow("89");

            AtRow(That.Contains, "89").Column(That.Contains, "Edit").Click("Edit");
            Set("Quantity code").To("030");
            Click("Save");
            WaitToSeeHeader("Second Quantity Descriptions");

            ExpectNoRow(That.Contains, "89");
            ExpectRow(That.Contains, "030");

            // Upload import
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");

            // ----------------------------------------------

            Click("New Import");
            WaitToSeeHeader("Upload Commodity Codes");
            Set("Choose file").To("Commodity.csv");
            Click("Save");

            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Pending");

            CheckBackgroundTasks();
            AtRow("Run Commodity Code Import Service").Click("Execute");
            Goto("/");

            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Partial success");

            // ----------------------------------------------


            Click("Settings");
            WaitToSeeHeader("Users");
            Click("Commodity Code Imports");
            WaitToSeeHeader("Commodity Code Imports");
            AtRow(That.Contains, "01/07/2021").Column("Import status").Expect("Partial success");

            AboveLink("Commodity Code Imports").Click("Commodity Codes");
            WaitToSeeHeader("Commodity Codes");

            // Assess code using active SQD
            Set("Find").To("20084051");
            Click("Search");

            ExpectRow("20084051");
            AtRow(The.Top, "20084051").Column("Second quantity").Expect("061");

            // Assess code using active SQD
            Set("Find").To("01012910");
            Click("Search");

            ExpectRow("01012910");
            AtRow("01012910").Column("Second quantity").ExpectNo("030");
        }
    }
}
