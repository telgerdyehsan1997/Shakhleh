using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompanyDetailsAssertGVMSAppearsWhenOutOfUKTicked : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies
            this.NavigateToCompanies();
            ClickLink("New Company");

            AtLabel("Transaction type").ClickCheckbox("Out of uk");
            ExpectLabel("GVMS");
        }
    }
}