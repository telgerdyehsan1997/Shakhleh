using MSharp;

namespace Domain
{
    class DiscountType : EntityType
    {
        public DiscountType()
        {
            InstanceAccessors("DateRangeDiscount");
            String("Name").Mandatory().Unique();
            String("Display Name").Mandatory();
        }
    }
}
