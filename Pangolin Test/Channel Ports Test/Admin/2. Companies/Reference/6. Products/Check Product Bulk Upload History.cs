using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckProductBulkUploadHistory : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<BulkUploadProducts>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            ExpectHeader("Companies");
            ExpectRow("Shipping Company Ltd");

            Click("Shipping Company Ltd");
            Click("Products");

            Click("Bulk Upload History");

            ExpectRow("ProductBulkUploadTestNew.csv");
            AtRow("ProductBulkUploadTestNew.csv").Column("Import status").Expect("PartialSuccess");
            AtRow("ProductBulkUploadTestNew.csv").Column("Errors").ClickLink();

            AtRow(The.Top).Expect("4");
            AtRow("4").Expect("Please provide a value for Commodity code.");
            AtRow("5").Expect("The provided Additional code is not a valid Integer text (digits only).");
            AtRow("6").Expect("The provided Additional code is too short. A minimum of 4 characters is expected.");
            AtRow("7").Expect("The provided Quota is not a valid Integer text (digits only).");
            AtRow("8").Expect("The provided Quota is too short. A minimum of 6 characters is expected.");
            AtRow("9").Expect("Please provide a value for VAT.");
            AtRow("10").Expect(@"Could not convert ""TRUTH"" to type System.Boolean.");
        }
    }
}