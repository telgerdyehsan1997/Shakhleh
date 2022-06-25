using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminClosesSupportTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<AdminClaimsSupportTicket>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Support Tickets
            this.NavigateToSupportTickets();
            ExpectRow(ticket.TrackingNumber);

            //Places the Support Ticket on Hold
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Click("Close");

            //Asserts that the ticket has been closed
            ExpectNoRow(ticket.TrackingNumber);
        }
    }
}