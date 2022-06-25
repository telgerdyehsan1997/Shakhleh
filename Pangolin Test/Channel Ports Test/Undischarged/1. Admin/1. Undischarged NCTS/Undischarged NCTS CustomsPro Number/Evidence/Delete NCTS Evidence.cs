using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_DeleteNCTSEvidence : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customsProNumber = "CP100009901";
            var fileName = "ProductBulkUploadTestNew.csv";

            Run<Undischarged_AddNCTSEvidence>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            Click("Undischarged NCTS");
            ExpectHeader("Undischarged NCTS");

            ExpectRow(customsProNumber);
            AtRow(customsProNumber).Column("Customs Pro Number").Click(customsProNumber);

            WaitToSeeHeader("Evidence Upload");
            ExpectRow(fileName);
            AtRow(fileName).Column("Delete").Click("Delete");
        }
    }
}