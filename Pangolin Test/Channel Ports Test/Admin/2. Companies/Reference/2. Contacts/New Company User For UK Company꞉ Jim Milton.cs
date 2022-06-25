using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewCompanyUserForUKCompanyJimMilton : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "UK COMPANY";

            Run<AddCompanyUKCompany_DefermentByDeposit>();
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
            Set("First name").To("Jim");
            Set("Last name").To("Milton");
            Set("Email address").To("jim.milton@uat.co");
            Set("Telephone number").To("07804221979");
            AtLabel("Accounts department").ClickLabel("Yes");
            AtLabel("Customer Admin").ClickLabel("Yes");
            ClickButton("Save");

            //Asserts that User has been created
            ExpectHeader("Company Users");
            ExpectRow("Jim");

            //Creates a password for the new User
            CheckMailBox("jim.milton@uat.co");
            AtRow("Welcome to Channel Ports").ClickLink();
            ExpectHeader("Subject: Welcome to Channel Ports");
            ClickLink("Set Your Password");
            ExpectHeader("Set Your Password");
            Set("Password").To("test");
            Set("Confirm new password").To("test");
            ClickButton("Save");
            ExpectHeader("JIM MILTON");
        }
    }
}