using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewEADMRNknownBulkConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewbulkNCTSShipmentOutOfUK>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Click("Search");

            AtRow("01/07/2019").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");

            //ExpectNo("Complete");
            //Expect("New Bulk Consignment");

            //Click("New Bulk Consignment");
            ExpectHeader(That.Contains, "Bulk Consignment Details");
            Set("EAD MRN").To("69GB56789012345678");
            Click("Search");

            Set("Total packages").To("5");
            Set("Total gross weight").To("10.98");
            Set("Total net weight").To("2.49");
            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect("12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click("12121212 - 14");
            Set("Description of goods").To("PACKAGES");
            Click("Save");

            ExpectHeader("Bulk Consignments");
        }
    }
}