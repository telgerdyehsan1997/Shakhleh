using MSharp;

namespace Domain
{
    class SupportTicketStatus : EntityType
    {
        public SupportTicketStatus()
        {
            TableName("TicketStatus");
            IsEnumReference();
            InstanceAccessors("Active",
                              "Closed");
            LogEvents(false);

            String("Name").Mandatory();

        }
    }
}