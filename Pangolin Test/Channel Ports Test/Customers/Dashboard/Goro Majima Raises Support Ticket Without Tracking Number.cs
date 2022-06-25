using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GoroMajimaRaisesSupportTicketWithoutTrackingNumber : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyUserToMajimaConstructionMajima>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            ClickLink("Dashboard");

            ExpectHeader("Dashboard");

            //Raises the Support Ticket without Tracking Number
            ClickLink("Raise Support Request");
            ExpectHeader("Raise Support Request");
            Set("Details").To("No Tracking Number");
            ClickButton("Save");

            //Assert that support Ticket has been raised
            ExpectHeader("Dashboard");
            ExpectRow("No Tracking Number");
        }
    }
}