using MSharp;

namespace Domain
{
    class Discount : EntityType
    {
        public Discount()
        {
            this.Archivable();

            String("Name");
            Percent("Percent").IsPercentage(false);
            Money("Amount").IsCurrency(false);
            Associate<Shop>("Shop").Mandatory();

            Bool("Is Food Specific").Mandatory();
            AssociateManyToMany<Food>("Discounted Foods");
            Bool("Is User Specific").Mandatory();
            AssociateManyToMany<ShopCustomer>("Discount Receivers");

            Money("Minimum Amount Of Price To Use").IsCurrency(false);
            Money("Maximum Amount Of Price To Use").IsCurrency(false);


            Date("Start");
            Date("End");

            Associate<DiscountCalculationType>("Calculation Type").Mandatory();
            Associate<DiscountType>("Type");
        }
    }
}
