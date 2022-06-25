using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithsShipmentListShowsTodaysShipmentsByDefault : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithCreatesACustomerAccount>();

            //add shipment 1 on date X
            //("01/01/2020");
            Run<AddNewShipmentForTruckersLtd>();

            //add shipment 2 on date Y
            //("01/02/2020");
            Run<AddAnotherShipmentForTruckers1MonthLater>();

            //assume date X; check shipment 1 (and not 2) show by default
            LoginAs<JohnSmithCustomer>();
            AssumeDate("01/01/2020");
            Goto("/");
            WaitToSeeHeader(That.Contains, "Shipment");
            ExpectRow(That.Contains, "R0120000001");
            ExpectNoRow(That.Contains, "R0220000001");

            //assume date Y; check shipment 2 (and not 1) show by default
            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/02/2020");
            Goto("/");
            WaitToSeeHeader(That.Contains, "Shipment");
            ExpectNoRow(That.Contains, "R0120000001");
            ExpectRow(That.Contains, "R0220000001");
        }
    }
}