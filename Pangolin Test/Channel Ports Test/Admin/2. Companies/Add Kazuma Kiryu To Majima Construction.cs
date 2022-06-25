using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddKazumaKiryuToMajimaConstruction : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);

            //Navigates to Contacts
            ClickLink("Contacts");
            ExpectHeader("Contacts");
            ClickLink("New Contact");
            ExpectHeader("Contact details");

            //Adds the Contact
            Set("First name").To("Kazuma");
            Set("Last name").To("Kiryu");
            Set("Email address").To("Kazuma.Kiryu@uat.co");
            ClickButton("Save");

            //Asserts that Contact has been saved
            ExpectRow("Kazuma");
        }
    }
}