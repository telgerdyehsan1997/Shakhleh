using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsConsignmentToCFSPShipment_SequenceNumber : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = "R0721000001";
            var consignment = new Constants.ConsignmentFactory().AddCFSPConsignment();

            Run<AdminCreatesShipmentWithCFSPCompany>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(shipment);

            //Navigates to Consignments
            AtRow(shipment).Column("Consignments").ClickLink();
            ClickLink("New Consignment");

            //Creates the Consignment
            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            //Adds Consignment Detail
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            this.ClickAndWait("Partner name", consignment.PartnerName);
            Set("Declarant").To("");
            System.Threading.Thread.Sleep(1000);
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            this.ClickAndWait("Declarant", consignment.Declarant);
            Set("Sequence number").To("123456");
            Set("Total packages").To(consignment.TotalPackages);
            Set("Total gross weight").To(consignment.TotalGrossWeight);
            Set("Total net weight").To(consignment.TotalNetWeight);
            Set("Invoice number").To(consignment.InvoiceNumber);
            this.ClickAndWait("Invoice currency", consignment.InvoiceCurrency);
            Set("Total value").To(consignment.TotalValue);
            Set("Terms of Sale").To(consignment.TermsOfSale);
            Click(What.Contains, "Save");

            //Asserts the Consignment has been saved
            ClickLink("Shipments");

            ExpectHeader("Shipments");
            this.FindShipment(shipment);
            AtRow(shipment).Column("Consignments").ClickLink("1");

            ExpectRow(consignment.TrackingNumber);
        }
    }
}