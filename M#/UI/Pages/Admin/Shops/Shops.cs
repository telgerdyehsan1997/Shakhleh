using MSharp;
using Domain;

namespace Admin.Shops
{
    public class ShopsPage : SubPage<FoodShopsPage>
    {
        public ShopsPage()
        {
            Add<Modules.ShopList>();
        }
    }
}