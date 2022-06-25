using MSharp;

namespace Domain
{
    class AccountingNotification : EntityType
    {
        public AccountingNotification()
        {
            DateTime("Notification Date").Mandatory().Default(cs("LocalTime.Now"));
            String("Error").Mandatory();
            Associate<Shipment>("Shipment").Mandatory().DatabaseIndex();
            Associate<SupportTicketAction>("Action").Mandatory().Default("c#:SupportTicketAction.Unclaim").DatabaseIndex();
            Associate<SupportTicketStatus>("Status").Mandatory().Default("c#:SupportTicketStatus.Active").DatabaseIndex();
            String("ClaimedBy");


        }
    }
}