using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportTransitOfficesFromFile : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");

            Click("Offices of Transit");

            WaitToSeeHeader("Offices of Transit");
            Click("Bulk Import");
            WaitToSeeHeader("Import History");

            Click("Import");
            Expect(What.Contains, "Import");
            Set("Choose file").To("OfficeTransitTest.csv");
            Click("Save");
            System.Threading.Thread.Sleep(2000);

            ExpectNoHeader("Import");

            RefreshPage();
            ExpectRow(That.Contains, "01/07/2021");
            ExpectNo("Errors");
            Click("Back");
            ExpectHeader("Offices of Transit");
            ExpectRow("CENTRAL CUSTOMS OFFICE");
            ExpectRow("DCNJ PORTA");
        }
    }
}