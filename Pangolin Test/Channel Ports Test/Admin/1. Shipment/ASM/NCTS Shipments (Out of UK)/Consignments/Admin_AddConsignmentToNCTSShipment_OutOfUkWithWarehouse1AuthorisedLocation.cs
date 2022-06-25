using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddConsignmentToNCTSShipment_OutOfUkWithWarehouse1AuthorisedLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321";
            var primaryContact = "ALAN SMITH";
            var authorisedLocation = "WAREHOUSE 1";
            var shipmentRoute = "Southampton to VALENCIA";
            var officeOfDestination = "IT IT IT112345 ITALY";
            var countryOfDestination = "Spain";
            var invoiceCurrency = "EUR";
            var commodityCode = "12121212";

            Run<AddRouteSouthamptonAndValencia, AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddWarehouse1AsTruckersAuthorisedLocation, AddAuthorisedLocationsToCompanyTruckers, CreateNewOfficeOfTransit_Italy>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");
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

            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("2223");
            Set("Trailer number").To("4242");

            Set("Expected date of departure").To("15/07/2022");

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

            AtLabel("Use authorised location").ClickLabel("Yes");
            ExpectLabel("Select authorised location");
            ClickField("Select authorised location");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, authorisedLocation);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, authorisedLocation);

            ClickButton("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");
            Set("EAD MRN").To("12GB45678945612349");
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

            Set("Total packages").To("10");
            Set("Total gross weight").To("145");
            Set("Total net weight").To("75");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, invoiceCurrency);

            Set("Total value").To("2000");

            ClickButton("Save and Add Commodities");


            ClickLink("New Commodity");
            ExpectHeader(That.Contains, "Commodity Details");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, commodityCode);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, commodityCode);

            Set("Description of goods").To("Heavy goods");
            Set("Gross weight").To("145");
            Set("Net weight").To("75");
            AtLabel("Currency").Expect("EUR");
            Set("Value").To("2000");
            Set("Number of packages for this commodity code (if known)").To("10");
            Click("Save");

            Click("Complete");

            ClickLink("NCTS Shipments Out of UK");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(The.Top).Column("Tracking number").ClickLink("1000000");
            ExpectNoButton("Transmit");
            Expect("On Site");

            AtRow("CP100000001").Column("UK Trader").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Partner").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Guarantor").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Country of destination").Expect("ES");
            AtRow("CP100000001").Column("Total packages").Expect("10");
            AtRow("CP100000001").Column("Total gross weight").Expect("145 kg");
            AtRow("CP100000001").Column("Total net weight").Expect("75 kg");
            AtRow("CP100000001").Column("Invoice currency").Expect("EUR");
            AtRow("CP100000001").Column("Total value").Expect("2,000.00");
            AtRow("CP100000001").Column("Commodities").Expect("1");
        }
    }
}
