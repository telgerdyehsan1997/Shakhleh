using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewBulkShipment_CommoditiesFoundViaERN : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRouteBlackpoolAndCalais, AdminAddsCompanyTruckersLtd, AddNewContactForTruckers_AlanSmith>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment Details");

            ExpectNo("Consignor");
            ExpectNo("Consignee");
            //  ExpectNo("Guarantor");
            ExpectNo("LRN");

            //Set shipment details to bulk shipment
            AtLabel("Is this a bulk Shipment").ClickLabel("Yes");
            Click("Channel Ports - Hythe - CT21 4BL - GB683470514001");
            Set("Guarantor").To("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Click("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Set("LRN").To("AB2");
            Set("Company name").To("Truckers Ltd");
            WaitToSee(What.Contains, "Truckers");
            Click(What.Contains, "Truckers");
            Set("Primary contact").To("Ala");
            WaitToSee("Ala");
            Click(What.Contains, "Ala");

            ClickLabel("Not required");

            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("t37");
            Set("Trailer number").To("t37");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7913456789");

            Set("Expected date of departure").To("10/07/2019");
            Set("Route").To("Cal");
            Click(What.Contains, "Blackpool");

            ExpectNoLabel(That.Equals, "Authorised location");

            Set("OfficeOfDestination").To("United Kingdom");
            Click("Save and Add/Amend Consignments");
            ExpectHeader(That.Contains, "Consignments");
            Click("New Bulk Consignment");

            Set("EAD MRN (if known)").To("ABCDE12345");
            Click("Search");

            AtLabel("Total Packages").Expect("2");
            AtLabel("Total gross weight").Expect(What.Contains, "5");
            AtLabel("Total gross weight").Expect(What.Contains, "kg");
            AtLabel("Total net weight").Expect(What.Contains, "3");
            AtLabel("Total net weight").Expect(What.Contains, "kg");

            AtLabel("Total Packages").Expect("2");
            AtLabel("Total Packages").Expect("2");
            AtLabel("Total Packages").Expect("2");

            Click("Back to Shipments");



            ExpectHeader("NCTS Shipments Out of UK");

            Set("to").To("01/07/2020");
            Click("Search");

            AtRow("10/07/2019").Column("Date").Expect("01/07/2019");
            AtRow("10/07/2019").Column("Expected date of departure").Expect("10/07/2019");
            AtRow("10/07/2019").Column("Route").Expect("Blackpool to Calais");
            AtRow("10/07/2019").Column("Customer Reference").Expect("30111");
            AtRow("10/07/2019").Column("Company name").Expect("Truckers Ltd");
            AtRow("10/07/2019").Column("Vehicle number").Expect("t37");
            AtRow("10/07/2019").Column("Trailer number").Expect("t37");
            AtRow("10/07/2019").Column("Progress").Expect("Draft");
        }
    }
}
