using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAccountNumberAndSortCodeToAcountingInformation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd";
            var accountNumber = "12345678";
            var sortCode = "654321";

            LoginAs<ChannelPortsAdmin>();

            //NAvigate to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");

            //Navigate to Company Accounting Information
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Accounting Information");

            //Set Account Number and Sort Code
            Set("Account Number").To(accountNumber);
            Set("Sort Code").To(sortCode);

            ClickButton("Save");
        }
    }
}