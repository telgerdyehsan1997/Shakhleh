using MSharp;

namespace Domain
{
    class DDPType : EntityType
    {
        public DDPType()
        {
            IsEnumReference();
            InstanceAccessors("Duty Inclusive", "Duty and VAT Inclusive");
            LogEvents(false);
            SortableByOrder();
            String("Name").Mandatory().Unique();
        }
    }
}