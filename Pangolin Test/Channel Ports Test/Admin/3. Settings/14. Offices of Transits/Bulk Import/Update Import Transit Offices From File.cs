using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UpdateImportTransitOfficesFromFile : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ImportTransitOfficesFromFile>();

            LoginAs<ChannelPortsAdmin>();
            Click("Settings");

            Click("Offices of Transit");

            WaitToSeeHeader("Offices of Transit");
            Click("Bulk Import");
            WaitToSeeHeader("Import History");

            Click("Import");
            Expect(What.Contains, "Import");
            Set("Choose file").To("OfficeTransitTestUpdate.csv");
            Click("Save");
            System.Threading.Thread.Sleep(2000);

            ExpectNoHeader("Import");

            RefreshPage();
            ExpectNo("Errors");
            Click("Back");
            ExpectHeader("Offices of Transit");
            ExpectRow("Not central customs office");
            ExpectRow("FR002511");
        }
    }
}