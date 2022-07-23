using MSharp;
using Domain;

namespace ShopUser
{
    public class SettingsPage : SubPage<ShopUserPage>
    {
        public SettingsPage()
        {
            Set(PageSettings.LeftMenu, "ShopUserSettingsMenu");

            OnStart(x => x.Go<Settings.FoodsPage>().RunServerSide());
        }
    }
}