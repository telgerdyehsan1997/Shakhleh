using MSharp;

namespace Domain
{
    class Response : EntityType
    {
        public Response()
        {
            String("Sender");
            DateTime("Date").Default("c#: LocalTime.Now").DatabaseIndex();
            BigString("Message").Mandatory();
            Associate<SupportTicket>("SupportTicket").Mandatory();
            InverseAssociate<ResponseAttachment>("Attachments", "Response");
            Associate<User>("User").Mandatory().DatabaseIndex();
            Bool("Is confirm").Mandatory().Default("false");

        }
    }
}