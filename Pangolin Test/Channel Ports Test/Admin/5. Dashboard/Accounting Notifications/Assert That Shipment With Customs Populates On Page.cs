using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatShipmentWithCustomsPopulatesOnPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();

            Run<AdminManuallyUpdatesShipmentStatusToQueried>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Accounting Notifications
            ClickLink("Dashboard");
            ExpectHeader("Support Tickets");
            ClickLink("Accounting Notifications");
            ExpectHeader("Accounting Notifications");

            //Asserts that Shipment populates in page
            ExpectRow(shipment.TrackingNumber);
            AtRow(shipment.TrackingNumber).Column("Task").Expect("Queried");
        }
    }
}