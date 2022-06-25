using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UKCompany_AddOverdraft : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "UK COMPANY";

            Run<AddCompanyUKCompany_DefermentByDeposit>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            ClickLink("Companies");

            ExpectHeader("Companies");
            AtRow(companyName).Column("Company name").ClickLink(companyName);

            ExpectHeader(companyName);

            //Navigates to Accounting Information
            ClickLink("Accounting Information");

            ExpectHeader("Accounting Information");
            AtLabel("Send invoices to Accounting Department").ClickLabel("Yes");

            //Sets the Overdraft Amount
            Set("Overdraft Amount").To("500");
            ClickButton("Save");

            //Navigates to Deposits to see if Overdraft has been saved
            ClickLink("Deposits");

            ExpectHeader("Deposits");
            AtLabel("Overdraft Amount").Expect("£ 500.00");
        }
    }
}