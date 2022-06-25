using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentToMajimaConstruction_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignment = new Constants.ConsignmentFactory().AddMajimaConstructionConsignmentIntoUK();

            Run<AddShipmentForMajimaConstruction_IntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipment to add Consignment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Consignments").ClickLink("0");

            ExpectLink("New Consignment");
            ClickLink("New Consignment");

            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            //Adds the Consignment
            ClickField("UK trader");
            Expect(What.Contains, consignment.UKTrader);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, consignment.UKTrader);
            this.ClickAndWait("Partner name", consignment.PartnerName);
            this.ClickAndWait("Declarant", consignment.Declarant);
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