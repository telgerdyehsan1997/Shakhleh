using MSharp;

namespace Domain
{
    class DiscountCalculationType : EntityType
    {
        public DiscountCalculationType()
        {
            InstanceAccessors("Percentage", "Amount");
            String("Name").Mandatory().Unique();
            String("Display Name").Mandatory();
        }
    }
}
