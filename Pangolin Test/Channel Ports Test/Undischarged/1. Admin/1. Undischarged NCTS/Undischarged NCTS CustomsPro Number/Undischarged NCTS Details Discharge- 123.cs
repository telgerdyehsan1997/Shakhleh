using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UndischargedNCTSDetailsDischarge_123 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customsProNumber = "CP100009901";

            Run<AddUndischargedNCTS_FillingForm_Amazon>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            AtRow(customsProNumber).Column("Customs Pro Number").ClickLink();

            Click("Discharge");

            ExpectHeader("Discharge");

            Set("Date").To("01/03/2022");

            AtLabel("Override Email Recipients").ClickLabel("Yes");

            Set("Email Recipients").To("test1@uat.co,test2@uat.co");

            Click("Save");

            Click("Undischarged NCTS");

            AtRow(customsProNumber).Column("Status").Expect("Discharge");
        }
    }
}