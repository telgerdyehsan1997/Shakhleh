using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertCCEmailsReceiveClosedNotification : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<SupportTicketRaisedWithCCAdded>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Support Tickets
            this.NavigateToSupportTickets();
            ExpectRow(ticket.TrackingNumber);

            //Claims and Closes the Support Ticket
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Click("Claim");
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Click("Close");

            //Asserts that the ticket has been closed
            ExpectNoRow(ticket.TrackingNumber);

            //Checks if Ticket Notification has been added to those CC'd in
            CheckMailBox("");
            ExpectRow("TEST1@UAT.CO");
            ExpectRow("TEST2@UAT.CO");
            ExpectRow("TEST3@UAT.CO");
        }
    }
}