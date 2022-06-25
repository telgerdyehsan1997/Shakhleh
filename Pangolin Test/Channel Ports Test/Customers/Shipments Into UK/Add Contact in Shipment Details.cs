using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddContactInShipmentDetails : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsShipmentForTruckersLtd>();

            LoginAs<JohnSmithCustomer>();

            AssumeDate("1/1/2019");
            Goto("/");

            WaitToSeeHeader(That.Contains,"Shipments Into UK");
            
            Click("Search");
            ClickLabel(The.Top, "All");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            RightOf("Primary contact").Click("AddContact");
            WaitForPopup();
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
            Set("First name").To("Jack");
            Set("Last name").To("Stevens");
            Set("Email address").To("jackstevens85@uat.co");
            Set("Telephone number").To("1234");
            Set("Mobile number").To("5678");
            Click("Save");

            //assert name is in list
            WaitToSeeHeader("Shipment Details");
            Near("Primary contact").Expect("Jack Stevens");

            //click Edit again, assert new contact is saved
            Click("Save and Add/Amend Consignments");
            Click(The.Top, "Shipments Into UK");
            ClickLabel(The.Top, "All");
            
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            BelowLabel("Primary contact").ExpectValue("Jack Stevens");

            // assert they exist in company contacts
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            ClickLink(That.Contains, "Truckers");
            Click("Contacts");
            ExpectRow("jackstevens85@uat.co");
        }
    }
}