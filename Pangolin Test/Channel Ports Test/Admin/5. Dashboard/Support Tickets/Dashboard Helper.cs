using Pangolin;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    public static class DashboardHelper
    {
        public static void NavigateToSupportTickets(this UITest @this)
        {
            @this.ClickLink("Dashboard");
            @this.ExpectHeader("Support Tickets");
        }

        public static void NavigateToCustomerDashboard(this UITest @this)
        {
            @this.ClickLink("Dashboard");
            @this.ExpectHeader("Dashboard");
        }
        public static void RaiseSupportTicket(this UITest @this, Tickets ticket)
        {
            @this.ExpectHeader("Raise Support Request");
            @this.AtLabel("Full Name").Expect(ticket.FullName);
            @this.AtLabel("Email").Expect(ticket.Email);
            @this.AtLabel("Phone").Expect(ticket.Phone);
            @this.Expect(The.Left, ticket.TrackingNumber);
            @this.Set("Details").To(ticket.TicketDetails);
            @this.ClickButton("Save");
        }

        public static void RaiseResponseToTicket(this UITest @this, Responses response)
        {
            @this.ClickLink("New Response");
            @this.ExpectHeader("Response details");
            @this.Set("Message").To(response.Message);
            @this.ClickButton("Save");
            @this.ExpectRow(response.Message);
        }
    }
}