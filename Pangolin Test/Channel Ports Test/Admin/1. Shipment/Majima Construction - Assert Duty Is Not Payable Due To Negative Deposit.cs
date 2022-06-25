using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class MajimaConstruction_AssertDutyIsNotPayableDueToNegativeDeposit : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();
            var consignment = new Constants.ConsignmentFactory().AddMajimaConstructionConsignmentOutOfUK();

            Run<AddNegativeDepositToMajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(shipment.TrackingNumber);

            //Navigate to Commodity Page for Shipment
            AtRow(shipment.TrackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignment.ConsignmentNumber);
            AtRow(consignment.ConsignmentNumber).Column("Commodities").ClickLink("1");

            //Completes the Shipment and asserts that Duty is not payable
            ClickButton("Complete");
            ExpectHeader("Your deposit does not have sufficient balance to process this shipment");
            Expect(What.Contains, "Please arrange to top up your deposit so this shipment can be processed or in the event of a problem contact CustomsPro");
        }
    }
}