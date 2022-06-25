using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SearchForCompanyWithSpecialCharactersPAndL : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "P&L UK LTD.";

            Run<AddCompanyPAndLUKLTD>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();

            //Searches for the Company with special Characters
            Set("Company name").To(companyName);
            ClickButton("Search");
            ExpectRow(companyName);
        }
    }
}