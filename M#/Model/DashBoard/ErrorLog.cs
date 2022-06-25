using MSharp;

namespace Domain
{
    class ErrorLog : EntityType
    {
        public ErrorLog()
        {
            DateTime("Recieved Date").Mandatory().Default(cs("LocalTime.Now"));
            String("FileName");
            SecureFile("File");
            String("Error").Mandatory();
            String("Location");
            Associate<Shipment>("Shipment").DatabaseIndex();
            Associate<SupportTicketAction>("Action").Mandatory().Default("c#:SupportTicketAction.Unclaim").DatabaseIndex();
            Associate<SupportTicketStatus>("Status").Mandatory().Default("c#:SupportTicketStatus.Active").DatabaseIndex();
            String("ClaimedBy");


        }
    }
}