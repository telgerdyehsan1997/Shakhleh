using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyUserToMajimaConstructionMajima : UITest
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

            //Creates new Company User
            ClickLink("Company Users");
            ExpectHeader("Company Users");
            ClickLink("New Company User");
            ExpectHeader("Company User Details");
            Set("First name").To("Goro");
            Set("Last name").To("Majima");
            Set("Email address").To("goro.majima@uat.co");
            Set("Telephone number").To("07804659222");
            AtLabel("Accounts department").ClickLabel("No");
            AtLabel("Customer Admin").ClickLabel("Yes");
            ClickButton("Save");

            //Asserts that User has been created
            ExpectHeader("Company Users");
            ExpectRow("Goro");

            //Creates a password for the new User
            CheckMailBox("goro.majima@uat.co");
            AtRow("Welcome to Channel Ports").ClickLink();
            ExpectHeader("Subject: Welcome to Channel Ports");
            ClickLink("Set Your Password");
            ExpectHeader("Set Your Password");
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            ClickButton("Save");
            ExpectHeader("GORO MAJIMA");
        }
    }
}