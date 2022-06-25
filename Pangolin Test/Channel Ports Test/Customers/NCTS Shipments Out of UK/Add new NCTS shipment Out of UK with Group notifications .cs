using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewNCTSShipmentOutOfUKWithGroupNotifications : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount, AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddRouteSouthamptonAndValencia, CreateNewTransitOfficePL>();
            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS shipment");
            ExpectHeader("Shipment Details");

            Set("Is this a bulk shipment?").To("No");
            ClickField("Primary contact");
            Type("Alan Smi");
            Click("Alan Smith");

            ClickLabel("Group");
            ExpectNoLabel("Contact name");

            Set(The.Bottom, "Group").To("IMPORT");

            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("22car");
            Set("Trailer number").To("4242");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7912345678");

            Set("Expected date of departure").To("15/07/2019");
            Set("Route").To("South");
            Click(What.Contains, "Southampton");

            ExpectNoLabel(That.Equals, "Authorised location");
            RightOfLabel(That.Contains, "EU port of arrival / Transit").Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");

            Set("Office of Destination").To("PL SZCZECIN PL987654 POLAND");

            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");
            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            Click("Search");

            AtRow("15/07/2019").Column("Date").Expect("01/07/2019");
            AtRow("15/07/2019").Column("Expected date of departure").Expect("15/07/2019");
            AtRow("15/07/2019").Column("Route").Expect("Southampton to Valencia");
            AtRow("15/07/2019").Column("Customer Reference").Expect("41222");
            AtRow("15/07/2019").Column("Company name").Expect("Truckers Ltd");
            AtRow("15/07/2019").Column("Vehicle number").Expect("22car");
            AtRow("15/07/2019").Column("Trailer number").Expect("4242");
            AtRow("15/07/2019").Column("Progress").ExpectText("Draft");

        }
    }
}
