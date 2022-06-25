using MSharp;

namespace Domain
{
    class ShipmentType : EntityType
    {
        public ShipmentType()
        {
            IsEnumReference();
            InstanceAccessors("Into Uk", "Out Of Uk");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();
            String("Display name");
        }
    }
}