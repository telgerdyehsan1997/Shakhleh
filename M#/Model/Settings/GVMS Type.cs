using MSharp;

namespace Domain
{
    class GVMSType : EntityType
    {
        public GVMSType()
        {
            IsEnumReference();
            InstanceAccessors("Always", "Sometimes", "Not GVMS");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();
        }
    }
}