using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentIntoUK_API : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRoutePortsmouthToCalais, AdminAddsCompanyTruckersLtd, CreateNewCompanyUser_JohnSmith, AddNewContactGroup_Import, EditTruckersToBeForwarder, AdminAddsProduct_IPad>();
            LoginAs<ChannelPortsAdmin>();

            //add shipment
            ExpectHeader("Shipments");
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CALAIS to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Portsmouth");

            Set("Customer Reference").To("B2356");
            Set("Vehicle number").To("45698");
            Set("Trailer number").To("456465");
            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("16/12/2020");
            System.Threading.Thread.Sleep(1000);

            Click(What.Contains, "Save");

            //add consignment
            ExpectHeader("Consignment Details");
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

            Set("Total packages").To("2");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("456789");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Total value").To("1000");
            Set("Terms of Sale").To("FAS - Free Alongside Ship");
            Click("Save and Add Commodities");

            //add commodities

            ClickLink("New Commodity");


            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "iPod 32GB - ABS00003");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "iPod 32GB - ABS00003");
            Set("Gross weight").To("450");
            Set("Net weight").To("400");
            Set("Second quantity").To("10");
            Set("Third quantity").To("10");
            Set("Value").To("450");
            Set("Number of packages for this commodity code (if known)").To("1");

            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ES - Spain");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES - Spain");
            Click("Save");

            ExpectRow("ABS00003");

            ClickLink("New Commodity");

            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "iPod 64GB - ABS00004");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "iPod 64GB - ABS00004");
            Set("Gross weight").To("550");
            Set("Net weight").To("500");
            Set("Second quantity").To("10");
            Set("Value").To("550");
            Set("Number of packages for this commodity code (if known)").To("1");
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ES - Spain");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES - Spain");
            Click("Save");

            ExpectRow("ABS00003");
            ExpectRow("ABS00004");

            ClickButton("Complete");
        }
    }
}
