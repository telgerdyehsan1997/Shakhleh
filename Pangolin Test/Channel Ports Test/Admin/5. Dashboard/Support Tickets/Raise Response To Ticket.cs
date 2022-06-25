using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class RaiseResponseToTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.AdminSupportTicket);
            var response = new Responses(Responses.AdminResponse);

            Run<RiseTicketSupport>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Support Tickets
            this.NavigateToSupportTickets();

            //Navigates to Ticket Responses
            AtRow(ticket.TrackingNumber).Column("Responses").ClickLink();
            ExpectHeader($"{ticket.TrackingNumber} Responses");

            //Raises the Response
            this.RaiseResponseToTicket(response);

            //Asserts that the Response has been raised
            AtRow(response.Message).Column("Date/Time").Expect(What.Contains, response.DateTime);
            AtRow(response.Message).Column("Sender").Expect(response.Sender);
            AtRow(response.Message).Column("Message").Expect(response.Message);
        }
    }
}