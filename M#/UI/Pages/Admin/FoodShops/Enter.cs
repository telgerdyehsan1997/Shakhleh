using MSharp;
using Domain;
using Admin.FoodShops;

namespace Admin.FoodShops
{
    public class EnterPage : SubPage<ShopsPage>
    {
        public EnterPage()
        {
            Add<Modules.FoodShopForm>();
        }
    }
}