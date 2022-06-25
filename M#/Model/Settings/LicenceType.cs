using MSharp;

namespace Domain
{
    class LicenceType : EntityType
    {
        public LicenceType()
        {
            IsEnumReference();
            InstanceAccessors("Electronic", "Paper");
            LogEvents(false);

            String("Name").Mandatory().Unique();
        }
    }
}