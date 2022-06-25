using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddCoupleOfShipmentsForDifferentCompanies : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddNewNCTSShipments_OutOfUK, AddShipentNCTSOutOfUKASMAcc>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            Expect("1000000");
            Expect("1000001");
        }
    }
}
