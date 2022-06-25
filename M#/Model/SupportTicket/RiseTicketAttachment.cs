using MSharp;

namespace Domain
{
    class RiseTicketAttachment : EntityType
    {
        public RiseTicketAttachment()
        {
            SecureFile("Attachment");
            Associate<SupportTicket>("SupportTicket");
        }
    }
}