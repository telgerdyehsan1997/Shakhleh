using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCommoditiesToSafetyAndSecurityConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<AddConsignmentToSafetyAndSecurityShipment>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Commodities of the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            AtRow("T072100000101").Column("Commodities").ClickLink();
            ExpectLink("New Commodity");
            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            this.AddCommodityDetails(
                commodityOut: true,
                productSelection: "MEAT - MEAT",
                productCode: "MEAT",
                grossWeight: "1",
                netWeight: "1",
                secondQuantity: "1",
                commodityValue: "1",
                numberOfPackages: "1",
                commodityCountry: "GR - Greece",
                countryPreference: "Yes",
                hazardousGoods: "No",
                hasHealthCertificateNumber: "Yes",
                healthCertificateNumber: "184",
                healthCertificateCode: "850-PLANT PRODUCTS"
                );
        }
    }
}