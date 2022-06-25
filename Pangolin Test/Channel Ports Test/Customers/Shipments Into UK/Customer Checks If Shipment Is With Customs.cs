using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerChecksIfShipmentIsWithCustoms : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();

            Run<AddCompanyUserToMajimaConstructionMajima, AdminManuallyUpdatesShipmentStatusToQueried>();
            LoginAs<Goro_MajimaConstruction>();

            //Finds the Shipment
            this.FindShipment(shipment.TrackingNumber);

            //Asserts that the progress is 'With Customs'
            AtRow(shipment.TrackingNumber).Column("Progress").Expect("With Customs");

        }
    }
}