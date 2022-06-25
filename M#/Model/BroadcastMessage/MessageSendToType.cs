using MSharp;

namespace Domain
{
    class MessageSendToType : EntityType
    {
        public MessageSendToType()
        {
            TableName("MessageSentToType");
            IsEnumReference();
            InstanceAccessors("Channelports",
                              "Customers",
                              "All");
            LogEvents(false);

            String("Name").Mandatory().Unique();

        }
    }
}