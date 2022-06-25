using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class UDEmailTemplatesList : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<Undischarged_ChannelPortsAdmin>();
            RefreshPage();

            //Navigates to Settings
            ClickLink("Settings");
            //ExpectHeader("Email Templates") Line can be Uncommented once the User page has been removed
            ClickLink("Email Templates"); //This line will need to be remvoed once the Email Templates page has been updated
            ExpectHeader("Email Templates");

            //Asserts that the Email Templates are pesent and are ordered Alphabetically
            ExpectRow("Charges");
            ExpectRow("Charges Shortages");
            ExpectRow("Discharged");
            ExpectRow("Evidence to Customs Before Discharge");
            ExpectRow("Evidence to Customs Before Discharge Shortages");
            ExpectRow("Forgot Password");
            ExpectRow("Recover Password");
            ExpectRow("Stage 1");
            ExpectRow("Stage 1 Shortages");
            ExpectRow("Stage 2");
            ExpectRow("Stage 2 Shortages");
            ExpectRow("Stage 3");
            ExpectRow("Stage 3 Shortages");
        }
    }
}