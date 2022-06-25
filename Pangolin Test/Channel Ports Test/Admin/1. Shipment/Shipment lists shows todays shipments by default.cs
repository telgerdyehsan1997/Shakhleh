using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ShipmentListsShowsTodaysShipmentsByDefault : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //add shipment 1 on date X
            //("02/01/2020");
            Run<AddShipmentForWWL, AddNewShipmentForTruckersLtd>();

            //add shipment 2 on date Y
            //("01/01/2020");
            //Run<AddNewShipmentForTruckersLtd>();

            //assume date X; check shipment 1 (and not 2) show by default
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("02/01/2020");
            Goto("/");
            WaitToSeeHeader(That.Contains, "Shipments");
            Set("Date created").To("30/12/1999");
            Set(The.Top, "to").To("05/01/2026");
            ClickButton("Search");
            ExpectRow(That.Contains, "T0721000001");
            //ExpectNoRow(That.Contains, "R0120000001");

            //assume date Y; check shipment 2 (and not 1) show by default
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/03/2020");
            Goto("/");
            WaitToSeeHeader(That.Contains, "Shipment");
            Set("Date created").To("30/12/1999");
            Set(The.Top, "to").To("05/01/2026");
            ClickButton("Search");
            //ExpectNoRow(That.Contains, "T0120000001");
            ExpectRow(That.Contains, "R0721000001");
        }
    }
}