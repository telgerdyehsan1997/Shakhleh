using MSharp;

namespace Domain
{
    class InvoiceType : EntityType
    {
        public InvoiceType()
        {
            InstanceAccessors("Transaction", "Charge");
            String("Name").Mandatory().Unique();
            String("Display Name").Mandatory();
        }
    }
}