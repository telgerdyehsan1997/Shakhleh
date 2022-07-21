using MSharp;

namespace Domain
{
    public class FoodShopCustomer : SubType<User>
    {
        public FoodShopCustomer()
        {

            InverseManyToMany<FoodShop>("Shops", "Customers");
        }
    }
}