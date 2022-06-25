using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminTriesToEditRejectedNCTSShipmentOutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddA2ndNCTSShipments_OutOfUK>();

            //update shipment status to be in rejected

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Expect("Edit");
        }
    }
}