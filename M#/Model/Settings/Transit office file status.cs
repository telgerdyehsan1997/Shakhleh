using MSharp;

namespace Domain
{
    class TransitOfficeFileStatus : EntityType
    {
        public TransitOfficeFileStatus()
        {
            IsEnumReference();
            InstanceAccessors("Successful", "Failed", "Partial success");
            LogEvents(false);
            SortableByOrder();

            String("Name").Mandatory().Unique();

        }
    }
}