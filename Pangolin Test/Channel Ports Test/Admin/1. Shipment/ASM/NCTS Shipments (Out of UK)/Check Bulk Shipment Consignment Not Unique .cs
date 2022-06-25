using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckBulkShipmentConsignmentNCTSNumberNotUnique : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddNewBulkNCTSShipments_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("01/07/2021").Column("Date").Expect("01/07/2021");
            AtRow("01/07/2021").Column("Expected date of departure").Expect("10/07/2021");
            AtRow("01/07/2021").Column("Route").Expect("Blackpool to Calais");
            AtRow("01/07/2021").Column("Customer Reference").Expect("30111");
            AtRow("01/07/2021").Column("Company name").Expect("Imports Ltd");
            AtRow("01/07/2021").Column("Vehicle number").Expect("t37");
            AtRow("01/07/2021").Column("Trailer number").Expect("t37");
            AtRow("01/07/2021").Column("Progress").Expect("Draft - Normal");
        }
    }
}
