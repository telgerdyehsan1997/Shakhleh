using MSharp;

namespace Domain
{
    class InvoiceFrequencyType : EntityType
    {
        public InvoiceFrequencyType()
        {
            IsEnumReference();
            InstanceAccessors("Monthly", "Yearly");

            String("Name");
        }
    }
}