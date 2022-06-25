using MSharp;

namespace Domain
{
    class CompanyType : EntityType
    {
        public CompanyType()
        {
            IsEnumReference();
            InstanceAccessors("Customer", "Flex", "Forwarder", "Other");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();
        }
    }
}