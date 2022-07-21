using MSharp;

namespace Domain
{
    public class FoodShopUser : SubType<User>
    {
        public FoodShopUser()
        {

            Bool("Is admin").Mandatory(value: false);

            Associate<FoodShop>("Shop");
        }
    }
}