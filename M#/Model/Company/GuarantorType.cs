using MSharp;

namespace Domain
{
    class GuarantorType : EntityType
    {
        public GuarantorType()
        {
            IsEnumReference();
            InstanceAccessors("Own", "DifferentCompanyGuarantee", "None");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();
            String("Display name");
        }
    }
}