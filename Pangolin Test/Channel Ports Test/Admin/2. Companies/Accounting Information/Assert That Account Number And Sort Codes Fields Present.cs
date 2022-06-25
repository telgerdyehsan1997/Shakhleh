using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatAccountNumberAndSortCodesFieldsPresent : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd";

            LoginAs<ChannelPortsAdmin>();

            //NAvigate to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");

            //Navigate to Company Accounting Information
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Accounting Information");

            //Assert that new fields are present
            ExpectHeader("Accounting Information");
            ExpectField("Account Number");
            ExpectField("Sort Code");
        }
    }
}