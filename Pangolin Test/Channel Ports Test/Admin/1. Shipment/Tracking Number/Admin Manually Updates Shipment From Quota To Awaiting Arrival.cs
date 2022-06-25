using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminManuallyUpdatesShipmentFromQuotaToAwaitingArrival : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();
            var consignment = new Constants.ConsignmentFactory().AddMajimaConstructionConsignmentOutOfUK();

            Run<AdminManuallyUpdatesShipmentStatusToManualQuota>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(shipment.TrackingNumber);

            //Clicks into the Tracking Number 
            AtRow(shipment.TrackingNumber).Column("Tracking number").ClickLink(shipment.TrackingNumber);

            ExpectHeader("Shipment Details");
            ExpectRow(consignment.ConsignmentNumber);

            //Updates the Status of the Consignment
            AtRow(consignment.ConsignmentNumber).Column("Progress").Click("Manual - Quota");
            ExpectHeader("Progress History");
            Set("Progress").To("AwaitingArrival");
            ClickButton("Save");

            //Asserts that Progress has been updated
            AtRow(consignment.ConsignmentNumber).Column("Progress").Expect("Awaiting Arrival");
        }
    }
}