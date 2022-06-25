using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentToSafetyAndSecurityShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<NewShipmentOutOfUK_SafetyAndSecurityEnabled>();
            LoginAs<ChannelPortsAdmin>();

            //Searches for the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Consignment page
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            ExpectLink("New Consignment");
            ClickLink("New Consignment");
            ExpectHeader("Consignment Details");

            //Sets Consignment Details
            this.AddConsignmentToShipment(
                consignmentNumber: "T072100000101",
                ukTrader: "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                partnerName: "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517",
                declarantName: "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                totalPackages: "1",
                totalGrossWeight: "1",
                totalNetWeight: "1",
                invoiceNumber: "Safety1",
                invoiceCurrency: "Great Britain - GBP",
                totalValue: "1",
                termsOfSale: "FAS - Free Alongside Ship"
                );
        }
    }
}