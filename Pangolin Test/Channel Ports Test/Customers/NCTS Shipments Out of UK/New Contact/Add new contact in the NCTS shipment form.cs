using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewContactInTheNCTSShipmentForm : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<AddNewConsignmentToNCTSShipment>();


            LoginAs<ChannelPortsAdmin>();
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow("1000000").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment details");
            AtRow("CP100000001").Column("Progress").Click("Ready to Transmit");
            Set("Progress").To("DraftNormal");
            ClickButton("Save");


            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Column("Edit").ClickLink();

            ClickButton("AddContact");

            ExpectHeader("New Contact");

            Set("First name").To("Terry");
            Set("Last name").To("Johnson");
            Set("Email address").To("tj@uat.co");
            Set("Telephone number").To("02071234567");
            Set("Mobile number").To("07612345678");

            Click("Save");

            ExpectHeader("Shipment Details");

            AtLabel("Primary contact").Expect("Terry Johnson");

            Click("Save and Add/Amend Consignments");

            LoginAs<ChannelPortsAdmin>();

            Click("Companies");

            AtRow("Truckers LTD").Column("Company name").ClickLink();
            Click("Contacts");
            AtRow("Terry").Column("Last name").Expect("Johnson");
            AtRow("Terry").Column("Email address").Expect("tj@uat.co");
            AtRow("Terry").Column("Telephone number").Expect("02071234567");
            AtRow("Terry").Column("Mobile number").Expect("07612345678");
        }
    }
}