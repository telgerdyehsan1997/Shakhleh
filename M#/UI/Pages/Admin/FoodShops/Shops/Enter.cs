using MSharp;
using Domain;

namespace Admin.FoodShops.Shops
{
    public class EnterPage : SubPage<ShopsPage>
    {
        public EnterPage()
        {
            Add<Modules.FoodShopForm>();
        }
    }
}