using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompanyDetailsAssertVATCodeOveride : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies
            this.NavigateToCompanies();
            AtRow("Imports Ltd").Column("Company name").ClickLink();
            ExpectHeader("Imports Ltd");


            //Assert that Select VAT Code appears when the check box is Ticked
            ClickCheckbox("Override VAT Code");
            ExpectField("Select VAT Code");

            //Assert that Select VAT Code disappears when the check box is Ticked
            ClickCheckbox("Override VAT Code");
            ExpectField("Select VAT Code");
        }
    }
}