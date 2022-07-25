using MSharp;

namespace Domain
{
    class DiscountFoodType : EntityType
    {
        public DiscountFoodType()
        {
            InstanceAccessors("AllFoodsButThereIsExclusion","OnlySpecifiedFoods");
            String("Name").Mandatory().Unique();
            String("Display Name").Mandatory();
        }
    }
}
