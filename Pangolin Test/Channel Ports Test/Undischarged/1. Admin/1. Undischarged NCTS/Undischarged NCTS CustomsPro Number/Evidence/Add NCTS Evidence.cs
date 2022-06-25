using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_AddNCTSEvidence : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customsProNumber = "CP100009901";
            var notes = "Hello world!";
            var fileName = "ProductBulkUploadTestNew.csv";

            Run<AddUndischargedNCTS_FillingForm_Amazon>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            Click("Undischarged NCTS");
            ExpectHeader("Undischarged NCTS");

            ExpectRow(customsProNumber);
            AtRow(customsProNumber).Column("Customs Pro Number").ClickLink();

            WaitToSeeHeader("Evidence Upload");

            ClickLink("Upload");
            ExpectHeader("Evidence Upload");

            Set("Choose file").To(fileName);
            Set("Notes").To(notes);
            ClickButton("Save");

            ExpectRow(fileName);
            AtRow(fileName).Column("File Name").Expect(fileName);
            AtRow(fileName).Column("Notes").Expect(notes);
        }
    }
}