using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddBulkConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";
            var primaryContact = "Jack Smith";
            var shipmentRoute = "Blackpool to CALAIS";
            var officeOfDestination = "ES MADRID ES001111 SPAIN";


            Run<OfficeOfTransitES, AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            ClickLink("New NCTS Shipment Out of UK");

            //Adds Shipment details
            ExpectHeader("Shipment details");
            AtLabel("Is this a bulk shipment?").ClickLabel("Yes");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, primaryContact);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, primaryContact);

            ClickField("Consignor");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Consignee");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);


            Set("Customer Reference").To("BulkConsignment");
            Set("Vehicle number").To("1234");
            Set("Expected date of departure").To("02/09/2021");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, shipmentRoute);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, shipmentRoute);

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, officeOfDestination);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, officeOfDestination);

            Click("Save and Add/Amend Consignments");

            //Adds the Consignment
            Set("EAD MRN").To("12GB45678945612349");
            ClickButton("Search");

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12345678 - 12");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12345678 - 12");


            Set("Description of goods").To("Goods");
            ClickButton("Save");
        }
    }
}