using Pangolin;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    public class Tickets
    {
        static Tickets Ticket;
        public Tickets() { }
        public Tickets(Tickets ticket)
        {
            FullName = ticket.FullName;
            Email = ticket.Email;
            Phone = ticket.Phone;
            TrackingNumber = ticket.TrackingNumber;
            TicketDetails = ticket.TicketDetails;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TrackingNumber { get; set; }
        public string TicketDetails { get; set; }

        public static Tickets AdminSupportTicket => Ticket ?? (Ticket = new Tickets
        {
            FullName = "Geeks Admin",
            Email = "admin@uat.co",
            Phone = "08004002343",
            TrackingNumber = "T0721000001",
            TicketDetails = "Raised Support Ticket",
        });

        public static Tickets CustomerSupportTicket => Ticket ?? (Ticket = new Tickets
        {
            FullName = "GORO MAJIMA",
            Email = "goro.majima@uat.co",
            Phone = "07804659222",
            TrackingNumber = "T0721000001",
            TicketDetails = "Raised Customer Support Ticket",
        });
    }
}