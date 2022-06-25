using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompanyDetailAssertAuthorisationNumber : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies
            this.NavigateToCompanies();
            ClickLink("New Company");

            AtLabel("Type").ClickLabel("Customer");
            AtLabel("Transaction type(s)").ClickLabel("Into uk");
            ExpectLabel("CFSP");
            //Clicks check box to assert Authorisation Number appears
            AtLabel("CFSP").ClickLabel("Own");
            ExpectField("Authorisation Number");

            //Unclicks CFSP Checkbox to assert that Authorisation Number no longer appears
            AtLabel("CFSP").ClickLabel("None");
            ExpectNoField("Authorisation Number");
        }
    }
}