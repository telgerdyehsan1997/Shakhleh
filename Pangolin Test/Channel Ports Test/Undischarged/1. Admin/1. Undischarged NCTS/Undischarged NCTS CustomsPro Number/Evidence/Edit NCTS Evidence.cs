using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_EditNCTSEvidence : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customsProNumber = "CP100009901";
            var notes = "Hello world!";
            var fileName = "ProductBulkUploadTestNew.csv";
            var editedFilename = "Commodity.csv";

            Run<Undischarged_AddNCTSEvidence>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            Click("Undischarged NCTS");
            ExpectHeader("Undischarged NCTS");

            ExpectRow(customsProNumber);
            AtRow(customsProNumber).Column("Customs Pro Number").Click(customsProNumber);

            WaitToSeeHeader("Evidence Upload");

            AtRow(fileName).Column("Edit").Click("Edit");
            ExpectHeader("Evidence Upload");

            Set("Choose file").To(editedFilename);
            ClickButton("Save");

            WaitToSeeHeader("Evidence Upload");

            ExpectNoRow(fileName);
            ExpectRow(editedFilename);
        }
    }
}