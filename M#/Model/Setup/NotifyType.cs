using MSharp;

namespace Domain
{
    class NotifyType : EntityType
    {
        public NotifyType()
        {
            IsEnumReference();
            InstanceAccessors("Not required", "Group", "Specific contacts");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();
        }
    }
}