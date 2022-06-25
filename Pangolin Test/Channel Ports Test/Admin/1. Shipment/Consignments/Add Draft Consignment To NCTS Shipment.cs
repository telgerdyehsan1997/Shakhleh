using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddDraftConsignmentToNCTSShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000000";
            var eADMRN = "12GB45678945612349";
            var ukTrader = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657";
            var partnerName = "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567";
            var countryOfDestination = "Italy";
            var invoiceCurrency = "Great Britain - GBP";

            Run<CreateDraftNCTSShipment>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipment
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Search for NCTS Shipment
            Set("Date created").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            ClickButton("Search");
            ExpectRow(trackingNumber);

            //Navigate to Create Consignment
            AtRow(trackingNumber).Column("Consignments").ClickLink("0");
            ExpectLink("New Consignment");
            ClickLink("New Consignment");
            ExpectHeader("Consignment Details");

            //Adds Consignment Details
            Set("EAD MRN").To(eADMRN);
            ClickButton("Search");

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

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, countryOfDestination);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, countryOfDestination);

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");
            Set("Total value").To("1");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, invoiceCurrency);

            ClickButton("Save and Add Commodities");
            ExpectLink("New Commodity");

            //Asserts that Consignment has been saved to Shipment
            ClickLink("NCTS Shipments Out of UK");
            this.FindNCTSShipment(trackingNumber);
            ExpectRow(trackingNumber);
            AtRow(trackingNumber).Column("Consignments").Expect("1");
        }
    }
}