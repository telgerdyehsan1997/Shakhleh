using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsCommoditiesToCFSPConsignment_SequenceNumber : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<AdminAddsConsignmentToCFSPShipment_SequenceNumber>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to Commodities
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");
            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink();
            ExpectLink("New Commodity");
            ClickLink("New Commodity");

            this.AddCommodityDetails(
                commodityOut: false,
                productSelection: "MONITORS - 12345678 - 12",
                productCode: "Greece",
                grossWeight: "1",
                netWeight: "1",
                secondQuantity: "1",
                commodityValue: "1",
                numberOfPackages: "1",
                commodityCountry: "GR - Greece",
                countryPreference: "Yes"
                );
        }
    }
}