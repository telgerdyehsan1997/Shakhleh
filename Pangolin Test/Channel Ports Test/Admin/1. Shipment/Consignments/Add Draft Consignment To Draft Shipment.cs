using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddDraftConsignmentToDraftShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var ukTrader = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657";
            var partnerName = "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567";
            var consignmentDeclarant = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657";
            var invoiceCurrency = "Great Britain - GBP";
            var termsOfSale = "FAS";

            Run<CreateDraftShipment>();
            LoginAs<ChannelPortsAdmin>();

            //Search for draft shipment
            Set("Date created").To("01/07/2021");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow(trackingNumber);

            //Navigate to Add Consignment
            AtRow(trackingNumber).Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            ClickButton("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");

            //Add the Consignment to the Shipment
            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, ukTrader);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, ukTrader);

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, partnerName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, partnerName);

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, consignmentDeclarant);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, consignmentDeclarant);

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");
            Set("Invoice number").To("1");
            Set("Total value").To("1");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, invoiceCurrency);

            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, termsOfSale);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, termsOfSale);

            ClickButton("Save and Add Commodities");
            ExpectLink("New Commodity");
        }
    }
}