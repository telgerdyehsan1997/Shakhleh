using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UndischargedNCTSDetailsStage2_123 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customsProNumber = "CP100009901";

            Run<AddUndischargedNCTS_FillingForm_Amazon>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            AtRow(customsProNumber).Column("Customs Pro Number").ClickLink();

            Click(What.Contains, "Stage 2");

            ExpectHeader("Stage 2");

            Set("Date").To("01/03/2022");

            ExpectNo("Email Recipients");

            Set("Override Email Recipients").To("No");

            Set("Email Recipients").To("test1@uat.co,test2@uat.co");

            Click("Save");

            Click("Undischarged NCTS");

            AtRow(customsProNumber).Column("Status").ExpectValue("Stage2");
        }
    }
}