using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_DeleteConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddA2ndConsignmentToAnNCTSShipment>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            AtRow("1000000").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment details");
            AtRow("CP100000001").Column("Progress").Click("Ready to Transmit");
            ExpectHeader("Progress History");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "DraftNormal");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "DraftNormal");
            Click("Save");
            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            AtRow("1000000").Column("Edit").ClickLink();

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            //ExpectHeader("Consignments");

            AtRow("CP100000001").Column("Delete").Click("Delete");
            Expect(What.Contains, "Are you sure you want to delete this consignment?");
            Click("OK");

            //ExpectNo("");
        }
    }
}