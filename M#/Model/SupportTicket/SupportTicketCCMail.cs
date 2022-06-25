using MSharp;

namespace Domain
{
    class SupportTicketCCMail : EntityType
    {
        public SupportTicketCCMail()
        {
            String("Email cc");

            Associate<SupportTicket>("Ticket")
               .DatabaseIndex();
        }
    }
}