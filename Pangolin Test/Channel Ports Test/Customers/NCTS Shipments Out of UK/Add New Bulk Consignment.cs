using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewBulkConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewbulkNCTSShipmentOutOfUK>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("Search");

            AtRow("01/07/2019").Column("Consignments").ClickLink("0");
            ExpectLink("New Bulk Consignment");
            ClickLink("New Bulk Consignment");

            ExpectHeader(That.Contains, "Bulk Consignment Details");
            Set("EAD MRN").To("12GB45678945612333");
            ClickButton("Search");
            Set("Total packages").To("5");
            Set("Total gross weight").To("10.98");
            Set("Total net weight").To("2.49");
            Set("Commodity code").To("12121212 - 14");
            Set("Description of goods").To("PACKAGES");

            Click("Save");

            ExpectHeader(That.Contains, "Bulk Consignment");
        }
    }
}