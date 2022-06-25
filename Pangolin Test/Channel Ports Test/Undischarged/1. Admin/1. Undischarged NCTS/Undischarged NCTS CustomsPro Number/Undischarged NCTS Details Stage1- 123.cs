using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UndischargedNCTSDetailsStage1_123 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddUndischargedNCTS_FillingForm_Amazon>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            AtRow("CP100009901").Column("Customs Pro Number").ClickLink();

            //ClickButton(That.Contains,"Stage 1");
            ClickXPath("/html/body/main/div[1]/div/div[2]/div[2]/form/div[2]/div/a[1]");

            ExpectHeader("Stage 1");

            Set("Date").To("01/03/2022");

            ExpectNo("Email Recipients");

            Set("Override Email Recipients").To("No");

            //Expect("Email Recipients");

            //Set("Email Recipients").To("test1@uat.co,test2@uat.co");

            Click("Save");

            Click("Undischarged NCTS");

            AtRow("CP100009901").Column("Status").Expect("Stage1");

            //CheckMailBox("test1@uat.co");
            //AtRow("test1@uat.co").Column("Subject").ClickLink();
            //ExpectHeader("Subject: Subject 3");


        }
    }
}