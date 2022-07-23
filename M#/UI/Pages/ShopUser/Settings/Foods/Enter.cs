using MSharp;
using Domain;

namespace ShopUser.Settings.Foods
{
    public class EnterPage : SubPage<FoodsPage>
    {
        public EnterPage()
        {
            Add<Modules.ShopUserFoodForm>();
        }
    }
}