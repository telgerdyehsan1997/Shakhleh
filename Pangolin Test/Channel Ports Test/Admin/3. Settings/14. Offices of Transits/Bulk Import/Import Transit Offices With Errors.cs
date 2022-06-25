using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportTransitOfficesWithErrors : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Offices of Transit");

            WaitToSeeHeader("Offices of Transit");
            Click("Bulk Import");
            WaitToSeeHeader("Import History");

            Click("Import");
            Expect("Import");
            Set("Choose file").To("TransitOfficeBulkUploadWithErrors.csv");
            Click("Save");
            System.Threading.Thread.Sleep(2000);
            WaitToSeeNoHeader("Import");

            RefreshPage();
            ExpectRow(That.Contains, "01/07/2019");
            AtRow(That.Contains, "01/07/2019").Expect("Failed");
            AtRow(That.Contains, "01/07/2019").Click("Errors");

            ExpectHeader(That.Contains, "Error Log");

            AtRow(That.Equals, "1").Column("Error reason").Expect("Usual name cannot be empty.");
            AtRow("2").Column("Error reason").Expect("The 8 digit Reference code must have first 2 characters alpha only, and the last 6 characters alphanumeric.");
            AtRow("3").Column("Error reason").Expect("At least one of the destination, departure or transit flags must be checked.");


        }
    }
}