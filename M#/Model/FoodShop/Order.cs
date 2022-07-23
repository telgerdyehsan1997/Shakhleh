using MSharp;

namespace Domain
{
    class Order : EntityType
    {
        public Order()
        {
            BigString("Details");
            DateTime("DateReceived").Mandatory();
            Money("Total price").Calculated().Getter("GetTotalPrice()");
            Associate<Shop>("Shop").Mandatory();
            Associate<ShopCustomer>("Customer");
            InverseAssociate<OrderItem>("FoodItems","Order");
        }
    }
}
