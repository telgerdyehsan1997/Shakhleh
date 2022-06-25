using MSharp;

namespace Domain
{
    class PaymentType : EntityType
    {
        public PaymentType()
        {
            String("Code").Mandatory().Unique();
            String("Description").Mandatory();

            this.Archivable();
        }
    }
}