using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class RiseTicketSupport : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.AdminSupportTicket);

            Run<AddShipmentForWWL>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(ticket.TrackingNumber);

            AtRow(ticket.TrackingNumber).Column("Actions").ExpectXPath("/html/body/main/div[1]/div/div/form/div[3]/div/div[2]/div/div[14]/div/button");
            AtRow(ticket.TrackingNumber).Column("Actions").ClickXPath("/html/body/main/div[1]/div/div/form/div[3]/div/div[2]/div/div[14]/div/button");
            ExpectLink("Raise Support Ticket");
            ClickLink("Raise Support Ticket");

            //Raises the Support Ticket
            this.RaiseSupportTicket(ticket);

            //Navigates to 'Support Ticket' page
            this.NavigateToSupportTickets();

            //Asserts that Support ticket has been raised
            ExpectRow(ticket.TrackingNumber);
            AtRow(ticket.TrackingNumber).Column("Task").Expect(ticket.TicketDetails);
        }
    }
}