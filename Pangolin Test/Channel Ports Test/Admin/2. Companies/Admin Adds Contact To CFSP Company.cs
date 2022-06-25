using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsContactToCFSPCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "CFSP OWN TEST";

            Run<AddCompany_CFSPSetToOwn>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);

            //Navigates to Contacts
            ClickLink("Contacts");
            ExpectHeader("Contacts");

            //Creates the new Contact
            ClickLink("New Contact");
            ExpectHeader("Contact details");
            Set("First name").To("CFSP");
            Set("Last name").To("Contact");
            Set("Email address").To("cfsp.contact@uat.co");
            ClickButton("Save");

            //Asserts that contact has been saved
            ExpectHeader("Contacts");
            ExpectRow("cfsp.contact@uat.co");
        }
    }
}