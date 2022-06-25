using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatProcessingErrorDisplaysAsTransmittedForCompanyUser : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<AddCompanyUserToMajimaConstructionMajima, AdminManuallyUpdatesShipmentStatusToProcessingError>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Shipments Out of the UK
            ClickLink("Shipments out of UK");

            ExpectHeader("Shipments Out of UK");

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Asserts that the Status of the Shipment is 'Transmitted'
            AtRow(trackingNumber).Column("Progress").Expect("Transmitted");
        }
    }
}