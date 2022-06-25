using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentToMajimaConstruction_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().CreateMajimaConstructionShipmentOutOfUK();
            var consignment = new Constants.ConsignmentFactory().AddMajimaConstructionConsignmentOutOfUK();

            Run<AddShipmentForMajimaConstruction_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipment to add Consignment
            this.FindShipment(shipment.TrackingNumber);
            AtRow(shipment.TrackingNumber).Column("Consignments").ClickLink("0");
            ExpectLink("New Consignment");
            ClickLink("New Consignment");
            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            //Adds the Consignment
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, consignment.UKTrader);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, consignment.UKTrader);
            this.ClickAndWait("Partner name", consignment.PartnerName);
            Set("Total packages").To(consignment.TotalPackages);
            Set("Total gross weight").To(consignment.TotalGrossWeight);
            Set("Total net weight").To(consignment.TotalNetWeight);
            Set("Invoice number").To(consignment.InvoiceNumber);
            Set("Total value").To(consignment.TotalValue);
            this.ClickAndWait("Invoice currency", consignment.InvoiceCurrency);
            Set("Terms of Sale").To(consignment.TermsOfSale);
            Click(What.Contains, "Save");
            ExpectLink("New Commodity");

            //Asserts that Consignment has been saved
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            ExpectRow(shipment.TrackingNumber);
            this.FindShipment(shipment.TrackingNumber);
            AtRow(shipment.TrackingNumber).Column("Consignments").Expect("1");
            AtRow(shipment.TrackingNumber).Column("Consignments").ClickLink("1");
            ExpectLink("New Consignment");
            ExpectRow(consignment.ConsignmentNumber);
        }
    }
}