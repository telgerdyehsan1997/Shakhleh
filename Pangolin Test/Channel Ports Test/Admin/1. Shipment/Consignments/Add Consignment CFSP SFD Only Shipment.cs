using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentCFSPSFDDisabledShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignment = new Constants.ConsignmentFactory().AddSFDCFSPConsignment();

            Run<AddShipment_CFSP_SFDOnlyDisabled>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipment to add Consignment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Consignments").ClickLink("0");

            ExpectLink("New Consignment");
            ClickLink("New Consignment");

            ExpectHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            //Adds the Consignment - As 'SFDOnly' is set to No in Company Details, we set the full declaration details
            this.ClickAndWait("Partner name", consignment.PartnerName);
            Set("Sequence number").To(consignment.SequenceNumber);
            Set("Total packages").To(consignment.TotalPackages);
            Set("Total gross weight").To(consignment.TotalGrossWeight);
            Set("Total net weight").To(consignment.TotalNetWeight);
            this.ClickAndWait("Invoice currency", consignment.InvoiceCurrency);
            Set("Invoice number").To(consignment.InvoiceNumber);
            Set("Total value").To(consignment.TotalValue);
            Set("Terms of Sale").To(consignment.TermsOfSale);
            Click(What.Contains, "Save");
            ExpectLink("New Commodity");

            //Asserts that Consignment has been saved
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            ExpectRow(trackingNumber);
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Consignments").Expect("1");
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");
            ExpectLink("New Consignment");
            ExpectRow(consignment.ConsignmentNumber);
        }
    }
}