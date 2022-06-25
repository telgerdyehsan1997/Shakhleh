using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNegativeDepositToMajimaConstruction : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION";
            var trackingNumber = "R072100000101";

            Run<AddCommodityForMajimaConstruction>();
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

            //Adds a deduction to the Deposit
            AtLabel("Transaction type").ClickLabel(The.Top, "Withdrawal");
            ExpectLabel(The.Bottom, "Withdrawal");
            Set("Date added").To("01/01/2021");
            Set(The.Bottom, "Withdrawal").To("100");
            ClickField("Tracking number");
            Expect(What.Contains, trackingNumber);
            Click(What.Contains, trackingNumber);
            Click("Save");

            //Assert Balance Value
            AtLabel("Remaining Balance").Expect("£0.00");
        }
    }
}