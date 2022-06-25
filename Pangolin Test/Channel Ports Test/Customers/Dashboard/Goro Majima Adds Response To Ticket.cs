using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GoroMajimaAddsResponseToTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            this.NavigateToCustomerDashboard();

            ExpectRow(ticket.TrackingNumber);
            AtRow(ticket.TrackingNumber).Column("Responses").ClickLink();

            //Raises the new response
            ExpectHeader($"{ticket.TrackingNumber} Responses");
            ClickLink("New Response");
            ExpectHeader("Response details");
            Set("Message").To("Company Response");
            ClickButton("Send");

            //Asserts that Response has been raised
            ExpectRow("Company Response");
        }
    }
}