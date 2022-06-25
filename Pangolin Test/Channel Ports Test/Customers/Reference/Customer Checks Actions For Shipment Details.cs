using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerChecksActionsForShipmentDetails : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";
            var consignmentNumber = "T072100000101";

            Run<AddCommoditiesToSafetyAndSecurityConsignment>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Shipments Out of UK
            ClickLink("Shipments Out of UK");
            ExpectHeader("Shipments Out of UK");

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Shipment Reference
            AtRow(trackingNumber).Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");
            ExpectRow(consignmentNumber);

            //Checks the Actions available to the Shipment
            AtRow(consignmentNumber).Column("Actions").Click("Select action");

            //Shipment is able to be edited as its Progress is 'Draft'
            Expect("Edit");
        }
    }
}