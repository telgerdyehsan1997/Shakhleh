using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [Ignore]
    [TestClass]
    public class CompanyDetailsAssertStatusChangeNotificationEmailField : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //The following option is no longer present in the Company Record page
            //Navigate to Companies
            this.NavigateToCompanies();
            ClickLink("New Company");
            ExpectHeader("Record Details Details");

            //Assert that Recieve status change notifications appears when check box is Ticked
            ClickCheckbox("Recieve status change notifications");
            ExpectField("Status Change Notification Email");

            //Assert that Recieve status change notifications no longer appears when check box is Unticked
            ClickCheckbox("Recieve status change notifications");
            ExpectNoField("Status Change Notification Email");
        }
    }
}