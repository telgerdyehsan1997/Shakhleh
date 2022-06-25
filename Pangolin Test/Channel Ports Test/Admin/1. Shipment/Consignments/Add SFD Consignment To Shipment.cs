using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddSFDConsignmentToShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<AdminCreatesShipmentWithCFSPCompany>();
            LoginAs<ChannelPortsAdmin>();

            //Find shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Consignments for the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            ExpectLink("New Consignment");
            ClickLink("New Consignment");

            //Adds the SFD Consignment to the Shipment
            this.AddSFDConsignment(
                ukTrader: "CFSP OWN TEST - LONDON - LND OA1 - GB683470514001",
                partnerName: "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517",
                declarantName: "CFSP OWN TEST - LONDON - LND OA1 - GB683470514001",
                cfspShipmentNumber: "1234567",
                totalPackages: "1",
                invoiceNumber: "SFD01"
                );
        }
    }
}