using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSShipment_AssertGrossAndNetWeightCantBe0 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";
            var primaryContact = "Jack Smith";
            var officeOfDestination = "IT IT IT112345 ITALY";
            var shipmentRoute = "Blackpool to CALAIS";
            var countryOfDestination = "Greece";
            var invoiceCurrency = "Great Britain - GBP";

            Run<AddRouteBlackpoolAndCalais, CreateNewOfficeOfTransit_Italy>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments
            ClickLink("NCTS Shipments Out of UK");
            ClickLink("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            //Sets Shipment Details
            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickHeader("Shipment details");
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

            Set("Customer Reference").To("NetGrossWeight");
            Set("Vehicle number").To("1234");
            Set("Expected date of departure").To("01/01/2023");

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

            ExpectHeader("Consignment Details");

            //Sets the consignment details
            Set("EAD MRN").To("12GB34567891234567");
            ClickButton("Search");

            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, countryOfDestination);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, countryOfDestination);

            Set("Total packages").To("1");

            //Sets the gross and net weight to 0
            Set("Total gross weight").To("0");
            Set("Total net weight").To("0");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, invoiceCurrency);
            Set("Total value").To("1");
            Click("Save and Add Commodities");

            Expect(What.Contains, "Total gross weight should be 0.01 or more.");
            Expect(What.Contains, "Total net weight should be 0.01 or more.");
        }
    }
}