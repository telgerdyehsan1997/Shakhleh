using MSharp;

namespace Domain
{
    class ShipmentBaseType : EntityType
    {
        public ShipmentBaseType()
        {
            IsEnumReference();
            InstanceAccessors("EAD", "NCTS");
            SortableByOrder();

            String("Name").Mandatory().Unique();
        }
    }
}