using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSharp.Framework;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentToTruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var ukTrader = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657";
            var partnerName = "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321";
            var declarantName = ukTrader;
            var totalPackages = "10";
            var totalGrossWeight = "100";
            var totalNetWeight = "90";
            var invoiceNumber = "AAAAA";
            var invoiceCurrency = "Great Britain - GBP";
            var totalValue = "1000";
            var termsOfSale = "FAS - Free Alongside Ship";

            Run<AdminAddsProduct_IPad, AddNewShipmentForTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();

            this.FindShipment(trackingNumber);

            AtRow(trackingNumber).Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click(What.Contains, "Save");
            //Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");

            //added by Rafal
            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(ukTrader);
            System.Threading.Thread.Sleep(1000);
            Click(ukTrader);

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, partnerName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, partnerName);

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(declarantName);
            System.Threading.Thread.Sleep(1000);
            Click(declarantName);

            Set("Total packages").To(totalPackages);
            Set("Total gross weight").To(totalGrossWeight);
            Set("Total net weight").To(totalNetWeight);
            Set("Invoice number").To(invoiceNumber);

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            Click(invoiceCurrency);

            Set("Total value").To(totalValue);

            Set("Terms of Sale").To(termsOfSale);

            Click(What.Contains, "Save");

            //ExpectHeader(That.Contains, "R071900000101 - Commodities");
            Expect("New Commodity");

        }
    }
}
