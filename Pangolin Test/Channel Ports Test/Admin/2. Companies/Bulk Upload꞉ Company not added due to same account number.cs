using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class BulkUploadCompanyNotAddedDueToSameAccountNumber : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<BulkUploadCompany>();

            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            ExpectHeader("Companies");

            Click("Bulk Upload");
            Set("Choose file").To("Duplicate company.csv");
            Click("Save");

            ExpectNoRow("Duplicate Inc");

            Click("Bulk Upload");
            Set("Choose file").To("Two same in import.csv");
            Click("Save");

            ExpectNoRow("Duplicate Inc");
            ExpectNoRow("Another one Inc");
        }
    }
}