using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class BulkUploadCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Companies");
            ExpectHeader("Companies");

            Click("Bulk Upload");
            Set("Choose file").To("BulkUploadTest.csv");
            Click("Save");

            CheckBackgroundTasks();
            AtRow("Run Company Bulk Import Service").Click("Execute");

            Goto("/");
            ClickLink("Companies");

            //New companies added
            ExpectRow("Channel Ports");
            ExpectRow("Imports Ltd");

            //Companies that failed validation are not added
            ExpectNoRow("Failure Ltd");

            //Existing companies are not edited
            AtRow("Imports Ltd").Column("Deferment number").Expect("6234517");

            AtRow("Imports Ltd").Column("Company name").ClickLink();
            Near("Customer account number").Expect("A5443");
            Near("Country").Expect("Spain");
            Near("Postcode/Zip code").Expect("AG2 YGD");
            Near("Address line 1").Expect("99 Dead End Road");
            Near("Town/City").Expect("Rome");
            Near("Branch identifier").Expect("BR543");
            Near("AEO number").Expect("03648392017584930213");
            Near("Deferment number").Expect("6234517");
            Near("Representation type").Expect("Indirect");
            Near("Default declarant").Expect("Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
        }
    }
}