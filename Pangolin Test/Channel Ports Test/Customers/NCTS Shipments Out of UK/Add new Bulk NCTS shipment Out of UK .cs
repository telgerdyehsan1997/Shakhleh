using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewbulkNCTSShipmentOutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount, AddNewContactForTruckers_AlanSmith, AddRouteBlackpoolAndCalais, CreateNewTransitOfficePL>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            Click("Search");

            Click("New NCTS Shipment");
            ExpectHeader("Shipment Details");
            AtLabel("Is this a bulk shipment?").ClickLabel("Yes");

            ClickHeader("Shipment Details");
            ClickField("Consignor");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Consignee");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALAN SMITH");

            ClickLabel("Not required");

            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("37AD");
            Set("Trailer number").To("t37");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7912345678");

            Set("Expected date of departure").To("10/07/2019");
            Set("Route").To("Cal");
            Click("Blackpool to Calais");

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "PL SZCZECIN PL987654 POLAND");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "PL SZCZECIN PL987654 POLAND");

            Click("Save and Add/Amend Consignments");
            ExpectHeader(That.Contains, "Bulk Consignment Details");

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            Click("Search");

            AtRow("01/07/2019").Column("Route").Expect("Blackpool to Calais");
            AtRow("01/07/2019").Column("Customer Reference").Expect("30111");
            AtRow("01/07/2019").Column("Company name").Expect("Truckers Ltd");
            AtRow("01/07/2019").Column("Vehicle number").Expect("37AD");
            AtRow("01/07/2019").Column("Progress").Expect("Draft");
        }
    }
}
