using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DownloadImportTemplate : UITest
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

            Click("Download template");
            ExpectDownloadedFile("TransitOfficeBulkUpload.csv");

            //manual - check the template
        }
    }
}