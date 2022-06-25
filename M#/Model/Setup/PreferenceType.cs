using MSharp;

namespace Domain
{
    class PreferenceType : EntityType
    {
        public PreferenceType()
        {
            IsEnumReference();
            InstanceAccessors("Invoice Declaration", "Preference Certificate Number" , "Statement of origin importers knowledge");
            LogEvents(false);
            SortableByOrder();

            String("Display Name").Mandatory();
            String("Name").Mandatory().Unique();
        }
    }
}