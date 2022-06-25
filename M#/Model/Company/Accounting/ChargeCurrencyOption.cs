using MSharp;

namespace Domain
{
    class ChargeCurrencyOption : EntityType
    {
        public ChargeCurrencyOption()
        {
            IsEnumReference();
            InstanceAccessors("Euro", "Pounds");

            String("Name").Mandatory();
        }
    }
}