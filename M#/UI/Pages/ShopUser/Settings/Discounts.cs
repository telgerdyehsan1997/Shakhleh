using MSharp;
using Domain;

namespace ShopUser.Settings
{
    public class DiscountsPage : SubPage<SettingsPage>
    {
        public DiscountsPage()
        {
            Set(PageSettings.LeftMenu, "ShopUserSettingsMenu");

            Add<Modules.ShopUserDiscountList>();
        }
    }
}