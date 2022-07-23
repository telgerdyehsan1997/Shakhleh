using MSharp;
using Domain;
using Admin.FoodShops;

namespace Admin.FoodShops
{
    public class DetailsPage : SubPage<ShopsPage>
    {
        public DetailsPage()
        {
            Set(PageSettings.LeftMenu, "AdminFoodShopsMenu");

            Add<Modules.FoodShopView>();
        }
    }
}