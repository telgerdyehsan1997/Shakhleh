using MSharp;

namespace Domain
{
    class Order : EntityType
    {
        public Order()
        {
            BigString("Details");
            DateTime("DateReceived").Mandatory();
            Int("Total price").Calculated().Getter("GetTotalPrice()");
            Associate<Shop>("Shop").Mandatory();
            Associate<ShopCustomer>("Customer");
            InverseAssociate<OrderItem>("FoodItems","Order");

            Associate<Discount>("Used discount");
            Int("Total price with discount");
            DateTime("Order date").Mandatory();
        }
    }
}
