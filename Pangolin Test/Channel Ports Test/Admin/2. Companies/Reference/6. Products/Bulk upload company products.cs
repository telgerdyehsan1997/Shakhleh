using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class BulkUploadCompanyProducts : UITest
    {
        [TestProperty("Sprint", "1")]
        [TestCategory("Bulk Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProductIPhone_Import,AdminAddsProduct_IPad>();
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");

            Click("Bulk Upload");
            Set("Choose file").To("ProductBulkUploadTestNew.csv");
            Click("Save");

        }
    }
}
