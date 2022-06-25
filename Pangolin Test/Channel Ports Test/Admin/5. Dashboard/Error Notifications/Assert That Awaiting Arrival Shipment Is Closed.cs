using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatAwaitingArrivalShipmentIsClosed : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();

            Run<AdminManuallyUpdatesShipmentFromQuotaToAwaitingArrival>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Accounting Notifications
            ClickLink("Dashboard");
            ExpectHeader("Support Tickets");
            ClickLink("Error Notifications");
            ExpectHeader("Error Logs");

            //Asserts that Notification is closed
            ExpectNoRow(shipment.TrackingNumber);
            AtLabel("Status").ClickLabel("Closed");
            ClickButton("Search");
            ExpectRow(shipment.TrackingNumber);
            AtRow(shipment.TrackingNumber).Column("Task").Expect("Manual - Quota");
            AtRow(shipment.TrackingNumber).Column("Actions").Expect("Re-Open");
        }
    }
}