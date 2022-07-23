using MSharp;
using Domain;

namespace Admin.Shops
{
    public class FoodsPage : SubPage<ShopsPage>
    {
        public FoodsPage()
        {
            Add<Modules.FoodList>();
        }
    }
}