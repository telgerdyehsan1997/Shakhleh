using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyUserForGVMSOnly : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "GVMS Only";

            Run<NewCompany_GVMSOnly>();
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
            Set("First name").To("GVMS");
            Set("Last name").To("User");
            Set("Email address").To("gvms.user@uat.co");
            AtLabel("Accounts department").ClickLabel("No");
            AtLabel("Customer Admin").ClickLabel("Yes");
            ClickButton("Save");

            //Asserts that User has been created
            ExpectHeader("Company Users");
            ExpectRow("GVMS");

            //Creates a password for the new User
            CheckMailBox("gvms.user@uat.co");
            AtRow("Welcome to Channel Ports").ClickLink();
            ExpectHeader("Subject: Welcome to Channel Ports");
            ClickLink("Set Your Password");
            ExpectHeader("Set Your Password");
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            ClickButton("Save");
            ExpectHeader("GVMS USER");
        }
    }
}