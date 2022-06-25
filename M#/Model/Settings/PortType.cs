using MSharp;

namespace Domain
{
    class PortType : EntityType
    {
        public PortType()
        {
            IsEnumReference();
            InstanceAccessors("GVMS", "Inventory");
            LogEvents(false);
            String("Name").Mandatory().Unique();
        }
    }
}