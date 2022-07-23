using MSharp;
using Domain;

namespace Admin.Shops.Details.Foods
{
    public class EnterPage : SubPage<FoodsPage>
    {
        public EnterPage()
        {
            Add<Modules.FoodForm>();
        }
    }
}