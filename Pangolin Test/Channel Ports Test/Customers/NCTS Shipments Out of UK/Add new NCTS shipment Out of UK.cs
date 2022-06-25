using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewNCTSShipmentOutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France, JohnSmithCreatesACustomerAccount, AddNewContactForTruckers_AlanSmith, CreateNewTransitOfficePL, AddRouteBlackpoolAndCalais>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS shipment");
            ExpectHeader("Shipment Details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            Set("Primary contact").To("Alan Smith");

            ClickLabel("Not required");
            ExpectNoField("Contact name");

            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("37AD");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7912345678");

            Set("Expected date of departure").To("10/07/2019");
            Set("Route").To("Cal");
            Click("Blackpool to Calais");
            RightOfLabel(That.Contains, "EU port of arrival / Transit").Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");
            Set(That.Contains, "Office of Destination").To("PL");
            Click(The.Bottom, "PL SZCZECIN PL987654 POLAND");

            ExpectNoLabel(That.Equals, "Authorised location");
            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");
            Click("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            Click("Search");

            AtRow("01/07/2019").Column("Date").Expect("01/07/2019");
            AtRow("01/07/2019").Column("Expected date of departure").Expect("10/07/2019");
            AtRow("01/07/2019").Column("Route").Expect("Blackpool to Calais");
            AtRow("01/07/2019").Column("Customer Reference").Expect("30111");
            AtRow("01/07/2019").Column("Company name").Expect("Truckers Ltd");
            AtRow("01/07/2019").Column("Vehicle number").Expect("37AD");
            //  AtRow("100000").Column("Trailer number").Expect("");
            AtRow("01/07/2019").Column("Progress").Expect("Draft");
        }
    }
}
