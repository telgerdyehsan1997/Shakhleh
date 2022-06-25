using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_DownloadNCTSEvidence : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //var customsProNumber = "CP100000001";
            var customsProNumber = "CP100009901";
            var fileName = "ProductBulkUploadTestNew.csv";

            Run<Undischarged_AddNCTSEvidence>();
            //LoginAs<Undischarged_ChannelPortsAdmin>();

            Click("Undischarged NCTS");
            ExpectHeader("Undischarged NCTS");

            ExpectRow(customsProNumber);
            AtRow(customsProNumber).Column("Customs Pro Number").ClickLink();

            WaitToSeeHeader("Evidence Upload");

            ExpectRow(fileName);
            AtRow(fileName).Column("Download").Click("Download");
        }
    }
}