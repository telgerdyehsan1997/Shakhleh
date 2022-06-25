using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SearchForNCTSShipmentUsingNewFilters : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddNewNCTSShipments_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Sets the search filters for the shipment
            Set("Tracking Number").To("1000000");
            Set("Route").To("Blackpool to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Set("Trailer number").To("T37");
            Set("Date Created").To("01/07/2021");
            Set(The.Top, "to").To("10/02/2022");
            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("T37");
            Set("Progress").To("Draft - Normal");
            ClickButton("Search");

            //Asserts that NCTS Shipment is returned
            ExpectRow("1000000");
        }
    }
}