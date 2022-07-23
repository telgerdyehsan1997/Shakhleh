using MSharp;
using Domain;

namespace Admin.Shops
{
    public class FoodsPage : SubPage<ShopsPage>
    {
        public FoodsPage()
        {
            Set(PageSettings.LeftMenu, "AdminShopsMenu");

            Add<Modules.FoodList>();
        }
    }
}