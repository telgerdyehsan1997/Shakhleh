using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckCompanyBulkUploadHistory : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<BulkUploadCompany>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            ExpectHeader("Companies");
            Click("Bulk Upload History");

            ExpectRow("BulkUploadTest.csv");
            AtRow("BulkUploadTestNew.csv").Column("Import status").Expect("PartialSuccess");
            AtRow("BulkUploadTestNew.csv").Column("Errors").ClickLink();

            AtRow(The.Top).Expect("3");
            AtRow("3").Expect("The provided Deferment number is too short. A minimum of 7 characters is expected.");
            AtRow("5").Expect("There is an existing Company with the same Deferment number, EORI number, Name, Postcode and Town/City in the database already.");
            AtRow("6").Expect("Please provide a value for Type.");
            AtRow("7").Expect(@"Customer account number must be in the format of ""A123456"".");
            AtRow("8").Expect("The EORI number field is required.");
            AtRow("9").Expect("Branch Identifier is in the wrong format.");
            AtRow("10").Expect("The provided AEO number is too short. A minimum of 20 characters is expected.");
            AtRow("11").Expect("Branch Identifier is in the wrong format.");
            AtRow("12").Expect("The provided Deferment number is too short. A minimum of 7 characters is expected.");
            AtRow("13").Expect("Branch Identifier is in the wrong format.");
            //AtRow("14").Expect("The provided TSP is too short. A minimum of 20 characters is expected.");
            AtRow("15").Expect("Branch Identifier is in the wrong format.");
            AtRow("16").Expect("The provided Guarantee type is too long. A maximum of 1 characters is acceptable.");


        }
    }
}