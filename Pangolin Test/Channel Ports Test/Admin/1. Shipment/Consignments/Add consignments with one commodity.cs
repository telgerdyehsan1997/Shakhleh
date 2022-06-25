using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentswithonecommodity : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126869")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewShipmentForTruckersLtd, AdminAddsProduct_IPad>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // Navigation
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            ClickButton("Search");
            AtRow(That.Contains, "25/12/2022").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            Set("Total packages").To("4");
            Set("Total gross weight").To("5.26");
            Set("Total net weight").To("4.992");
            Set("Invoice number").To("TRUCKERS-2019-1101");
            ClickField("Invoice Currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("500");
            ClickLabel("Only 1 Commodity");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");
            Click(What.Contains, "Save and Add Commodities");

            ClickLink("New Commodity");

            ExpectHeader("Commodity Details");
            ClickLabel("Product Code");
            System.Threading.Thread.Sleep(1000);
            Click("IPAD - ABS12343");
            Set("Gross weight").To("5.26");
            Set("Net weight").To("4.992");
            RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025 Litres");
            Set("Second quantity").To("100");

            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GR - Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            Set("Preference type").To("Preference certificate number");
            Set("Value").To("500");
            Set("Number of packages for this commodity code (if known)").To("4");
            Set(The.Bottom, "Preference certificate number").To("GR1258654588");
            Click("Save");

            //ExpectHeader(That.Contains, "R071900000101 - Commodities");
            //ExpectRowColumns(That.Contains, "ABS12343", "5.26 kg", "4.99 kg", "Great Britain - GBP", "500", "2", "Greece", "Yes");
            ExpectRow("ABS12343");
        }
    }
}
