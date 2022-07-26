using MSharp;

namespace Domain
{
    class Discount : EntityType
    {
        public Discount()
        {
            this.Archivable();

            String("Name");
            String("Description");
            Percent("Percent").IsPercentage(false);
            Int("Amount");
            Associate<Shop>("Shop").Mandatory();

            AssociateManyToMany<Food>("Discounted Foods");
            AssociateManyToMany<Food>("Excluded Foods");

            Bool("Is User Specific").Mandatory();
            AssociateManyToMany<ShopCustomer>("Discount Receivers");

            Money("Minimum Amount Of Price To Use").IsCurrency(false);
            Money("Maximum Amount Of Price To Use").IsCurrency(false);


            Date("Start");
            Date("End");

            Associate<DiscountCalculationType>("Calculation Type").Mandatory();
            Associate<DiscountFoodType>("Food Type");
        }
    }
}
