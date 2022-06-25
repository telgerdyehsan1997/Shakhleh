using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchivedUserNotVisibleInCustomerAdviceForm : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithCreatesACustomerAccount>();

            //admin adds contact to truckers 'Steve Newman'
            LoginAs<ChannelPortsAdmin>();
            Click(The.Top, "Companies");
            ExpectHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader(That.Contains, "Truckers Ltd");
            Click("Contacts");
            WaitToSeeHeader(That.Contains, "Contacts");
            Click("New Contact");
            WaitToSeeHeader(That.Contains, "Contact Details");
            Set("First name").To("Steve");
            Set("Last name").To("Newman");
            Set("Email address").To("stevenewman@uat.co");
            Click("Save");
            WaitToSeeHeader(That.Contains, "Contacts");
            ExpectRow(That.Contains, "Newman");

            //john checks Steve is in Advice form
            /*LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            Click("New Advice");
            WaitToSeeHeader(That.Contains, "Advice Details");
            Click(What.Contains, "Nothing selected");
            Type("Steve Newman");
            Expect(What.Contains, "Steve Newman");*/

            //admin archives Steve
            LoginAs<ChannelPortsAdmin>();
            Click(The.Top, "Companies");
            WaitToSeeHeader(That.Contains, "Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader(That.Contains, "Truckers Ltd");
            Click("Contacts");
            WaitToSeeHeader(That.Contains, "Contacts");
            AtRow(That.Contains, "Newman").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archived");
            ClickButton("Archive");
            WaitToSeeHeader(That.Contains, "Contacts");
            ExpectNoRow(That.Contains, "Newman");
            NearLabel("Active").ClickLabel("Archived");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Contacts");
            ExpectRow(That.Contains, "Newman");

            //john checks Steve is not in Advice form
            /*LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            Click("New Advice");
            WaitToSeeHeader(That.Contains, "Advice Details");
            Click(What.Contains, "Nothing selected");
            Type("Steve Newman");
            Expect(What.Contains, "No results matched \"Steve Newman\"");*/
        }
    }
}