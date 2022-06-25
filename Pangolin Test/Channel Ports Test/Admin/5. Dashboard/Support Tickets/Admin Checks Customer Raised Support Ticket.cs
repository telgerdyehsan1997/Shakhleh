using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminChecksCustomerRaisedSupportTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to 'Support Ticket' page
            this.NavigateToSupportTickets();

            //Asserts that Support ticket has been raised
            ExpectRow(ticket.TrackingNumber);
            AtRow(ticket.TrackingNumber).Column("Task").Expect(ticket.TicketDetails);
        }
    }
}