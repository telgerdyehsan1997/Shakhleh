using MSharp;

namespace Domain
{
    class OrderItem : EntityType
    {
        public OrderItem()
        {
            Int("Count").Min(1).Default("1").Mandatory();
            Associate<Order>("Order").Mandatory();
            Associate<Food>("Food").Mandatory();
        }
    }
}
