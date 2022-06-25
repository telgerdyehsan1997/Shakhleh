using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddConsignmentToNCTSShipment_OutOfUkWithNoAuthorisedLocationGB000060 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "100000";

            Run<AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddRoutePortsmouthToCalais>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            Set("Company name").To("Truck");
            Click("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");
            ClickField("Primary contact");
            Type("Alan Smit");
            Click("Alan Smith");

            System.Threading.Thread.Sleep(500);
            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("2223");
            Set("Trailer number").To("4242");
            //Set("Driver mobile country").To("FR (+33)");
            //Set("Driver mobile number").To("7912345678");

            Set("Expected date of departure").To("15/07/2019");
            Set("Route").To("Cal");
            Click("Portsmouth to Calais");

            ExpectNoLabel(That.Equals, "Authorised location");
            Click("Save and Add/Amend Consignments");
            ExpectHeader(That.Contains, "Consignments");

            Click("New Consignment");

            ExpectHeader("Consignment Details");

            Click(What.Contains, "Not Known");
            Set("LRN").To("R021900000113254765622");
            ClickField("UK trader");
            Type("Truckers");
            Click("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Partner name");
            Type("Truckers");
            Click("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Guarantor");
            Type("Truckers");
            Click("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");

            Set("Country of destination").To("United");
            Click("United Kingdom");

            Set("Total packages").To("10");
            Set("Total gross weight").To("145");
            Set("Total net weight").To("75");
            Set("Invoice currency").To("EU");
            Click("EUR");
            Set("Total value").To("2000");

            Click("Save and Add Commodities");

            ExpectHeader(That.Contains, "R021900000113254765622 - Commodities");

            Click("New Commodity");

            ExpectHeader(That.Contains, "Commodity Details");
            Set("Commodity code").To("1212121");
            Click("12121212");
            Set("Description of goods").To("Heavy goods");
            Set("Gross weight").To("145");
            Set("Net weight").To("75");
            AtLabel("Currency").Expect("EUR");
            Set("Value").To("4000");
            Set("Number of packages for this commodity code (if known)").To("10");
            Click("Save");

            ExpectHeader(That.Contains, "R021900000113254765622 - Commodities");

            Click("Complete");

            ExpectHeader(That.Contains, "Consignments");

            AtRow("R021900000113254765622").Column("UK Trader").Expect("Truckers ltd");
            AtRow("R021900000113254765622").Column("Partner").Expect("Truckers ltd");
            AtRow("R021900000113254765622").Column("Guarantor").Expect("Truckers ltd");
            AtRow("R021900000113254765622").Column("Country of destination").Expect("GB");
            AtRow("R021900000113254765622").Column("Total packages").Expect("10");
            AtRow("R021900000113254765622").Column("Total gross weight").Expect("145 kg");
            AtRow("R021900000113254765622").Column("Total net weight").Expect("75 kg");
            AtRow("R021900000113254765622").Column("Invoice currency").Expect("EUR");
            AtRow("R021900000113254765622").Column("Total value").Expect("2,000.00");
            AtRow("R021900000113254765622").Column("Commodities").Expect("1");

            AtRow("R021900000113254765622").Column("Commodities").Click("1");
            ExpectHeader(That.Contains, "R021900000113254765622 - Commodities");
            AtRow("12121212").Column("Edit").ExpectNoButton();
            AtRow("12121212").Column("Delete").ExpectNoButton();

            //check that the transmit button is visible

            Click("NCTS Shipments Out of UK");
            this.FindNCTSShipment(trackingNumber);
            AtRow(The.Top).Column("Tracking number").ClickLink("100000");
            ExpectNoButton("Transmit");
        }
    }
}
