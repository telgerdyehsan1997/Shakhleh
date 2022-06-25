using MSharp;

namespace Domain
{
    public class ArchiveLog : EntityType
    {
        public ArchiveLog()
        {
            BigString("Reason").Mandatory();
            String("User Ip").Mandatory();
            DateTime("Date and time").DefaultFormatString("dd/MM/yyyy HH:mm").Mandatory();
            Guid("EntityId");
            Associate<User>("Logged in user").Mandatory();
            String("Tracking Number");
        }
    }
}
