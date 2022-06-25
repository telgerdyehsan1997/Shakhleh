using MSharp;

namespace Domain
{
    class SupportTicketAction : EntityType
    {
        public SupportTicketAction()
        {
            TableName("TicketActions");
            IsEnumReference();
            InstanceAccessors("Claim",
                              "Unclaim",
                              "PlaceOnHold",
                              "HighlightToSupervisor",
                              "Respond",
                              "Close");
            LogEvents(false);

            String("Name").Mandatory();

        }

    }
}