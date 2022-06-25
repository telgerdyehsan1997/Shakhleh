using MSharp;

namespace Domain
{
    class SupportTicket : EntityType
    {
        public SupportTicket()
        {

            String("Ticket Number").Default("c#: new Random().Next(100000, 1000000).ToString()").Mandatory();
            Associate<Shipment>("Shipment").DatabaseIndex();

            Associate<Person>("User").Mandatory().DatabaseIndex();
            Associate<User>("Created by").Mandatory().DatabaseIndex();
            Associate<Company>("Company").DatabaseIndex();

            BigString("Task detail");
            DateTime("Notification time").Default("c#: LocalTime.Now").DatabaseIndex();
            String("Claimed by");

            InverseAssociate<Response>("Responses", "SupportTicket");
            InverseAssociate<RiseTicketAttachment>("Attachments", "SupportTicket");
            Associate<SupportTicketAction>("Action").Mandatory().Default("c#:SupportTicketAction.Unclaim").DatabaseIndex();
            Associate<SupportTicketStatus>("Status").Mandatory().Default("c#:SupportTicketStatus.Active").DatabaseIndex();

            InverseAssociate<SupportTicketCCMail>("EmailCC", "Ticket");

        }
    }
}