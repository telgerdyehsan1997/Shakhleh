using MSharp;
using Domain;

namespace ShopUser.Settings
{
    public class FoodsPage : SubPage<SettingsPage>
    {
        public FoodsPage()
        {
            Set(PageSettings.LeftMenu, "ShopUserSettingsMenu");

            Add<Modules.ShopUserFoodList>();
        }
    }
}