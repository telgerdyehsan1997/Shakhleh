using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GoroMajimaViewsClosedSupportTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<AdminClosesSupportTicket>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            this.NavigateToCustomerDashboard();

            //Sets status to 'Closed'
            AtLabel("Status").ClickLabel("Closed");
            ClickButton("Search");

            //Asserts closed ticket
            ExpectRow(ticket.TrackingNumber);
            AtRow(ticket.TrackingNumber).Column("Status").Expect("Closed");
        }
    }
}