using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertTheClearedShipmentIsClosed : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();

            Run<AdminManuallyUpdatesShipmentFromQueriedToCleared>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Accounting Notifications
            ClickLink("Dashboard");
            ExpectHeader("Support Tickets");
            ClickLink("Accounting Notifications");
            ExpectHeader("Accounting Notifications");

            //Asserts that Notification is closed
            ExpectNoRow(shipment.TrackingNumber);
            AtLabel("Status").ClickLabel("Closed");
            ClickButton("Search");
            ExpectRow(shipment.TrackingNumber);
            AtRow(shipment.TrackingNumber).Column("Task").Expect("Queried");
            AtRow(shipment.TrackingNumber).Column("Actions").Expect("Re-Open");
        }
    }
}