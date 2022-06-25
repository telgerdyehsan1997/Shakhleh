using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminManuallyUpdatesShipmentStatusToQueried : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();
            var consignment = new Constants.ConsignmentFactory().AddMajimaConstructionConsignmentOutOfUK();

            Run<AddConsignmentToMajimaConstruction_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(shipment.TrackingNumber);

            //Clicks into the Tracking Number 
            AtRow(shipment.TrackingNumber).Column("Tracking number").ClickLink(shipment.TrackingNumber);

            ExpectHeader("Shipment Details");
            ExpectRow(consignment.ConsignmentNumber);

            //Updates the Status of the Consignment
            AtRow(consignment.ConsignmentNumber).Column("Progress").Click("Draft");
            ExpectHeader("Progress History");
            Set("Progress").To("QueriedWithCustoms");
            ClickButton("Save");

            //Asserts that Progress has been updated
            AtRow(consignment.ConsignmentNumber).Column("Progress").Expect("Queried");
        }
    }
}