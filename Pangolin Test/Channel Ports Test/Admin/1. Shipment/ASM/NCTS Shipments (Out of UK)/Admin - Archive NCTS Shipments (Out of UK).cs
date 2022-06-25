using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_ArchiveNCTSShipments_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddA2ndNCTSShipments_OutOfUK>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");

            AtRow("Blackpool to CALAIS").Column("Archive").Click("Archive");

            ExpectHeader("Archive");
            ExpectLabel("Please Explain Why");
            Set("Please Explain Why").To("Archived NCTS Shipment");
            ClickButton("Archive");

            ExpectNo("100000");
        }
    }
}