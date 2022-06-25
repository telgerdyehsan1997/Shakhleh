using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompanyDetailsAssertSafetyInboundDefaultsToNo : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies
            this.NavigateToCompanies();
            ClickLink("New Company");

            //Asserts that Safety and security inbound defaults to 'No'
            AtLabel("Safety and security inbound").ExpectLabel("No safety and security");
            AtLabel("Safety and security outbound").ExpectLabel("No safety and security");
        }
    }
}