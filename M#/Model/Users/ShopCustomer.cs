using MSharp;

namespace Domain
{
    public class ShopCustomer : SubType<User>
    {
        public ShopCustomer()
        {

            InverseManyToMany<Shop>("Shops", "Customers");
        }
    }
}