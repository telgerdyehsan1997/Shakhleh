using MSharp;
using Domain;

namespace Admin.FoodShops
{
    public class ShopsPage : SubPage<FoodShopsPage>
    {
        public ShopsPage()
        {
            Add<Modules.FoodShopList>();
        }
    }
}