using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageNCTSShipments : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<ArchiveNCTSShipmentOutOfUK, AddNewNCTSShipmentOutOfUK>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");


            //status
            ClickLabel("All");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            Expect("1000000");
            Expect("1000001");

            ClickLabel("Active");
            Click("Search");

            ExpectNo("1000000");
            Expect("1000001");

            ClickLabel("Archived");
            Click("Search");

            Expect("1000000");
            ExpectNo("1000001");


            Click("NCTS Shipments Out of UK");
            ClickLabel("All");
            Click("Search");

            //find - LRN
            /*Click("Consignment Search");
            Set("LRN").To("R0219000001");
            Click(The.Left, "Search");
            Expect("1000000");
            ExpectNo("1000001"); */

            AtHeader(That.Contains, "Shipment Level Search Filter").ClickButton("Clear Shipment Level Search");
            //find - Date 
            Set("Date Created").To("02/07/2018");
            Set(The.Top, "to").To("01/01/2019");
            Click("Search");
            ExpectNo("1000000");
            ExpectNo("1000001");

            Set("Date Created").To("01/07/2019");
            Set(The.Top, "to").To("");
            Click("Search");
            ExpectNo("1000000");
            Expect("1000001");
            AtHeader(That.Contains, "Shipment Level Search Filter").ClickButton("Clear Shipment Level Search");
            Set("Date Created").To("");
            Set(The.Top, "to").To("");

            //find - expected date of departure
            Set("Expected date of arrival/departure").To("15/07/2019");
            Click("Search");
            ExpectNo("1000000");
            ExpectNo("1000001");
            Set("Expected date of arrival/departure").To("");

            //find - Route
            Set("Route").To("Blackpool to CALAIS");
            Click("Search");
            ExpectNo("1000000");
            Expect("1000001");
            AtHeader(That.Contains, "Shipment Level Search Filter").ClickButton("Clear Shipment Level Search");

            //find - Customer Reference
            Set("Customer Reference").To("41222");
            Click("Search");
            ExpectNo("1000000");
            ExpectNo("1000001");

            Set("Customer Reference").To("30111");
            Click("Search");
            ExpectNo("1000000");
            Expect("1000001");
            Set("Customer Reference").To("");

            //Unable to search by Company name
            /*//find - Company name
            Set("Find").To("Dell");
            Click("Search");
            ExpectNo("1000000");
            ExpectNo("1000001"); 

            Set("Find").To("Truckers Ltd");
            Click("Search");
            Expect("1000000");
            Expect("1000001"); */

            //find - Vehicle number
            Set("Vehicle number").To("22");
            Click("Search");
            ExpectNo("1000000");
            ExpectNo("1000001");

            Set("Vehicle number").To("37AD");
            Click("Search");
            ExpectNo("1000000");
            Expect("1000001");
            Set("Vehicle number").To("");

            //find - Trailer number
            Set("Trailer number").To("4242");
            Click("Search");
            ExpectNo("1000000");
            ExpectNo("1000001");

            //progress - not needed yet
        }
    }
}
