using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditShipmentToAddContact : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentForWWL>();

            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "T0721000001").Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            ClickLink("Edit");

            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");
            ClickLabel("Company type");
            Near("Primary contact").ClickButton("AddContact");
            WaitForPopup();
            WaitToSeeHeader("New Contact");

            //assert form layout
            BelowHeader("New Contact").Expect("First name");
            Below("First name").Expect("Last name");
            Below("Last name").Expect("Email address");
            Below("Email address").Expect("Telephone number");
            Below("Telephone number").Expect("Mobile number");
            Below("Mobile number").ExpectButton("Cancel");
            NearButton("Cancel").ExpectButton("Save");

            //add contact
            Set("First name").To("Bob");
            Set("Last name").To("Danson");
            Set("Email address").To("bobdanson@uat.co");
            Set("Telephone number").To("1234");
            Set("Mobile number").To("5678");
            Click("Save");

            //assert name is in list
            WaitToSeeHeader("Shipment Details");
            AtLabel("Primary Contact").Expect(What.Contains, "BOB DANSON");

            //click Edit again, assert new contact is saved
            Click("Save and Add/Amend Consignments");
            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            WaitToSeeHeader("Shipments");

            AtRow(That.Contains, "T0721000001").Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            ClickLink("Edit");

            WaitToSeeHeader("Shipment Details");
            AboveLabel("Specific contacts").ExpectValue("Bob Danson");

            //check Companies > Contacts for new contact
            Click("Companies");
            AtRow(That.Contains, "Worldwide Logistics Ltd").Column("Company name").ClickLink("Worldwide Logistics Ltd");
            WaitToSeeHeader("Worldwide Logistics Ltd");
            Click("Contacts");
            WaitToSeeHeader("Contacts");
            AtRow(That.Contains, "Danson").Column("First name").Expect("Bob");
            AtRow(That.Contains, "Danson").Column("Last name").Expect("Danson");
            AtRow(That.Contains, "Danson").Column("Email address").Expect("bobdanson@uat.co");
            AtRow(That.Contains, "Danson").Column("Telephone number").Expect("1234");
            AtRow(That.Contains, "Danson").Column("Mobile number").Expect("5678");
            AtRow(That.Contains, "Danson").Column("Notes").ExpectText("");
        }
    }
}