using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UndischargedNCTSDetailsProvideAdditionalInformation_123 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //var customsProNumber = "CP100000001";
            var customsProNumber = "CP100009901";
            var body = "Hello world!";
            var upload = "ProductBulkUploadTestNew.csv";

            Run<Undischarged_AddNCTSEvidence>();
            //LoginAs<Undischarged_ChannelPortsAdmin>();

            Click("Undischarged NCTS");
            ExpectHeader("Undischarged NCTS");

            ExpectRow(customsProNumber);
            //AtRow(customsProNumber).Column("CustomsPro Number").Click(customsProNumber);
            AtRow(customsProNumber).Column("Customs Pro Number").ClickLink();

            WaitToSeeHeader("Evidence Upload");

            ClickLink("Provide Additional Information");
            ExpectHeader("Provide Additional Information");

            Set("Body").To(body);
            Set(The.Top,"Choose file").To(upload);
            
            ClickButton("Save");

            Click("Undischarged NCTS");

            AtRow(customsProNumber).Column("Status").Expect("ProvideAdditionalInformation");
        }
    }
}