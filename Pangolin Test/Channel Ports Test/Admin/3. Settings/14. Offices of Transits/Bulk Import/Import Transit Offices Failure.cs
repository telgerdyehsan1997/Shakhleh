using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportTransitOfficesFailure : UITest
    {
        [TestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");

            Click("Offices of Transit");

            WaitToSeeHeader("Offices of Transit");
            Click("Bulk Import");
            WaitToSeeHeader("Import History");

            Click("Import");
            WaitToSeeHeader("Import");
            Set("File").To("TransitOfficeBulkUploadFailure.csv");
            Click("Save");

            WaitToSeeNoHeader("Import");

            RefreshPage();
            ExpectRow(That.Contains, "01/07/2019");
            AtRow(That.Contains, "01/07/2019").Expect("Failed");
            AtRow(That.Contains, "01/07/2019").Click("Errors");

            ExpectHeader(That.Contains, "Error Log");

            AtRow(That.Equals, "1").Column("Error reason").Expect("Name cannot be empty.");
            AtRow("2").Column("Error reason").Expect("The provided NCTS code is too short. A minimum of 8 characters is expected."); ;
            AtRow("3").Column("Error reason").Expect("At least one of the destination, departure or transit flags must be checked.");
            AtRow("4").Column("Error reason").Expect("Alias must be unique. There is an existing Transit office alias record with the provided Alias.");
            AtRow("5").Column("Error reason").Expect("Alias must be unique. There is an existing Transit office alias record with the provided Alias.");
        }
    }
}