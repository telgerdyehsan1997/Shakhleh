using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TryToCompleteShipmentWithNonMatchingCommodityValue : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRouteSouthamptonAndValencia, AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddStop24AsTruckersAuthorisedLocation, EditTruckersToBeForwarder>();
            LoginAs<ChannelPortsAdmin>();

            Click("Shipments");

            Click("New Shipment");


            ClickField("Company name");
            Expect("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            Click("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickLabel("Out of UK");
            AtLabel("NCTS").ClickLabel("No");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click("Alan Smith");

            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("2223");
            Set("Trailer number").To("4242");

            ClickHeader("Shipment Details");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Southampton to VALENCIA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Southampton to VALENCIA");

            Set("Expected date of departure").To("15/07/2022");
            /* Set("Port of departure").To("Sou");
             Click("Southampton"); */

            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");


            Set("UK trader").To("");
            ClickHeader("Consignment Details");
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
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            Set("Total packages").To("10");
            Set("Total gross weight").To("145");
            Set("Total net weight").To("75");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GREAT BRITAIN - GBP");
            Set("Total value").To("2000");
            Set("Invoice number").To("23");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");
            Click("Save and Add Commodities");

            Click("New Commodity");

            Click("AddProduct");
            Set("Code").To("1256");
            Set("Name").To("testProduct1");
            ClickField("Commodity Code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212 - 14");
            Click(The.Top, "Save");

            ClickHeader("Commodity Details");
            ClickField("Product Code");
            System.Threading.Thread.Sleep(1000);
            Expect(The.Top, "TESTPRODUCT1 - 1256");
            System.Threading.Thread.Sleep(1000);
            Click(The.Top, "TESTPRODUCT1 - 1256");
            Set("Gross weight").To("145");
            Set("Net weight").To("75");
            Set("Value").To("3999.99");
            Set("Number of packages for this commodity code (if known)").To("10");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(The.Top, "FR - FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click(The.Top, "FR - FRANCE");
            AtLabel("Preference").ClickLabel("Yes");

            Click("Save");
            ExpectHeader(That.Contains, "Commodities");
        }
    }
}
