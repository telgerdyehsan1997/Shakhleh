using MSharp;

namespace Domain
{
    class Discount : EntityType
    {
        public Discount()
        {
            this.Archivable();

            String("Name");
            Associate<Shop>("Shop").Mandatory();

            Bool("Is Food Specific").Mandatory();
            AssociateManyToMany<Food>("Discounted Foods");
            Bool("Is User Specific").Mandatory();
            AssociateManyToMany<ShopCustomer>("Discount Receivers");

            Money("Minimum Amount Of Price To Use").IsCurrency(false);
            Money("Maximum Amount Of Price To Use").IsCurrency(false);

            Associate<DiscountCalculationType>("Calculation Type").Mandatory();
            Associate<DiscountType>("Type").Mandatory();
        }
    }
}
