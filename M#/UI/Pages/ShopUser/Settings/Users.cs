using MSharp;
using Domain;

namespace ShopUser.Settings
{
    public class UsersPage : SubPage<SettingsPage>
    {
        public UsersPage()
        {
            Set(PageSettings.LeftMenu, "ShopUserSettingsMenu");

            Add<Modules.ShopUserUserList>();
        }
    }
}