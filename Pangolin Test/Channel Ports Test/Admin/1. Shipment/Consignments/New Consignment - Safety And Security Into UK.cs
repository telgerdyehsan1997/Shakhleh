using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewConsignment_SafetyAndSecurityIntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var shipment = new Constants.ShipmentFactory().MajimaConstrunctionSafetyAndSecurityIntoUK();
            var consignment = new Constants.ConsignmentFactory().AddSandSConsignmentIntoUK();

            Run<NewShipmentIntoUK_SafetyAndSecurityEnabled>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(shipment.TrackingNumber);

            //Navigates to the Consignment of the Shipment
            AtRow(shipment.TrackingNumber).Column("Consignments").ClickLink("0");
            ExpectLink("New Consignment");
            ClickLink("New Consignment");

            ExpectHeader("Consignment Details");

            //Sets the Consignment Details
            Set("UK trader").To("");
            System.Threading.Thread.Sleep(1000);
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            ClickField("UK trader");
            Expect(What.Contains, consignment.UKTrader);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, consignment.UKTrader);
            this.ClickAndWait("Partner name", consignment.PartnerName);
            Set("Total packages").To(consignment.TotalPackages);
            Set("Total gross weight").To(consignment.TotalGrossWeight);
            Set("Total net weight").To(consignment.TotalNetWeight);
            Set("Invoice number").To(consignment.InvoiceNumber);
            this.ClickAndWait("Invoice currency", consignment.InvoiceCurrency);
            Set("Total value").To(consignment.TotalValue);
            Set("Terms of Sale").To(consignment.TermsOfSale);
            Click("Save and Add Commodities");

            //Assert that the consignment has been saved
            ClickLink("Shipments");

            AtRow(shipment.TrackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignment.ConsignmentNumber);
        }
    }
}