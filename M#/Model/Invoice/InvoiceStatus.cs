using MSharp;

namespace Domain
{
    class InvoiceStatus : EntityType
    {
        public InvoiceStatus()
        {
            InstanceAccessors("InProgress", "NotSentToExchequer", "SentToExchequer", "NotSentToExchequerFailure");
            String("Name").Mandatory().Unique();
            String("Display Name").Mandatory();
        }
    }
}