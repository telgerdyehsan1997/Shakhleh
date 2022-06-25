using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatManualQuotaShipmentPopulatesOnPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();

            Run<AdminManuallyUpdatesShipmentStatusToManualQuota>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Accounting Notifications
            ClickLink("Dashboard");
            ExpectHeader("Support Tickets");
            ClickLink("Error Notifications");
            ExpectHeader("Error Logs");

            //Asserts that Shipment populates in page
            ExpectRow(shipment.TrackingNumber);
            AtRow(shipment.TrackingNumber).Column("Task").Expect("Manual - Quota");
        }
    }
}