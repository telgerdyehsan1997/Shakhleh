using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewDeposit_UKCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "UK COMPANY";
            var currentDate = "";

            Run<AddCompanyUKCompany_DefermentByDeposit>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Company
            this.NavigateToCompanies();
            AtRow(companyName).Column("Company name").Click(companyName);
            ExpectHeader(companyName);

            //Navigates to Deposits
            ClickLink("Deposits");
            ExpectHeader("Deposits");
            Click("New Deposit");
            ExpectHeader("Deposit Details");

            //Adds a Deposit
            AtLabel("Transaction type").ClickLabel(The.Top, "Deposit");

            //Asserts that the Date displays the current date as a Read-Only version
            //AtField("Date added").Expect("01/07/2021");
            Set(The.Bottom, "Deposit in").To("100");
            Click("Save");

            //Asserts that the Deposit has been added
            AtLabel("Remaining Balance").Expect("£100.00");
        }
    }
}