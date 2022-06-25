using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentWithDuplicateEADMRNAndImportEADCommodities : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipentNCTSOutOfUKASMAcc>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments Out of UK
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Adds new shipment
            ClickLink("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");
            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Jack Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Jack Smith");

            Set("Customer Reference").To("CU12345");
            Set("Vehicle number").To("123");
            Set("Trailer number").To("4321");
            Set("Expected date of departure").To("01/01/2023");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth");

            ExpectLabel("Office of Destination");
            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "PL");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "PL");
            ClickButton("Save and Add/Amend Consignments");

            //Adds the duplicate EAD MRN
            ExpectHeader("Consignment Details");
            Set("EAD MRN").To("12GB45678945612345");
            ClickButton("Search");

            //Fills in Consignment Details
            Set("UK Trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FRANCE");

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("1");
            ClickButton("Save and Add Commodities");

            //The following workflow no longer occurs
            /*Click("OK");
            AtRow("ALIENS").Column("Description of goods").Expect("ALIENS"); */
        }
    }
}