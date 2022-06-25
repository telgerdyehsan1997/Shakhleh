using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageBulkShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithCreatesACustomerAccount, AddNewContactForTruckers_AlanSmith, AddRouteBlackpoolAndCalais, AddStop24AsTruckersAuthorisedLocation, OfficeOfTransitES>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            Click("New NCTS shipment");
            ExpectHeader("Shipment Details");

            ExpectNo("Consignor");
            ExpectNo("Consignee");
            ExpectNo("LRN");

            AtLabel("Is this a bulk shipment?").ClickLabel("Yes");

            Expect("Consignor");
            Expect("Consignee");
            Expect("Guarantor");

            //Consignor should only show UK companies
            ClickLabel("Consignor");
            System.Threading.Thread.Sleep(1000);
            Expect("Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            ClickHeader("Shipment Details");

            //Consignee should only show non UK companies
            /* ClickLabel("Consignee");
             System.Threading.Thread.Sleep(1000);
             Expect("Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");
             Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
             Expect("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
             System.Threading.Thread.Sleep(1000);
             ClickHeader("Shipment details"); */

            // Set all details apart from LRN
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALAN SMITH");

            Set("Consignor").To("");
            ClickHeader("Shipment details");
            ClickField("Consignor");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Consignee");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickLabel("Not required");

            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("37AD");
            Set("Trailer number").To("t37");

            Set("Expected date of departure").To("10/07/2019");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "BLACKPOOL TO CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "BLACKPOOL TO CALAIS");
            //Set(That.Contains, "FirstBorderCrossing_Text").To("United Kingdom (GB) - GB000060");
            //Click(The.Bottom, "United Kingdom (GB) - GB000060");
            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ES MADRID ES001111 SPAIN");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES MADRID ES001111 SPAIN");
            //Click(The.Bottom, "United Kingdom (GB) - GB000060");

            ExpectNoLabel(That.Equals, "Authorised location");


            //Workflow no longer exists
            /* ExpectText(That.Contains, "The LRN field is required");
             Set("LRN").To("FGD558456/79541253");
            Click("Save and Add/Amend Consignments");
            Expect("LRN number only contain alphanumeric characters.");
            Click("Ok");

            Set("LRN").To("FGD55 8456587954125");
            Click("Save and Add/Amend Consignments");
            Expect("LRN number only contain alphanumeric characters.");
            Click("Ok");

            Set("LRN").To("FGD5584565879541253267553");
            Click("Save and Add/Amend Consignments");
            ExpectText(That.Contains, "LRN should not exceed 22 characters.");

            Set("LRN").To("FGD5584565879541253"); */

            //Use authorised location
            Below("Use authorised location").ExpectNo("Authorised location");
            AtLabel("Use authorised location").ClickLabel("Yes");
            Below("Use authorised location").ExpectLabel("Authorised location");
            Expect("Stop 24");

            Click("Save and Add/Amend Consignments");

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("01/07/2019").Column("Date").Expect("01/07/2019");
            AtRow("01/07/2019").Column("Expected date of departure").Expect("10/07/2019");
            AtRow("01/07/2019").Column("Route").Expect("Blackpool to Calais");
            AtRow("01/07/2019").Column("Customer Reference").Expect("30111");
            AtRow("01/07/2019").Column("Company name").Expect("Truckers Ltd");
            AtRow("01/07/2019").Column("Vehicle number").Expect("37AD");
            AtRow("01/07/2019").Column("Progress").Expect("Draft");

        }
    }
}
