using MSharp;

namespace Domain
{
    class SafetyAndSecurity : EntityType
    {
        public SafetyAndSecurity()
        {
            IsEnumReference();
            InstanceAccessors("Always", "Sometimes", "NoSafetyAndSecurity");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();
            String("Display name");
        }
    }
}