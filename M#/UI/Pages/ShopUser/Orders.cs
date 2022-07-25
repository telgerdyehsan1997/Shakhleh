using MSharp;
using Domain;

namespace ShopUser
{
    public class OrdersPage : SubPage<ShopUserPage>
    {
        public OrdersPage()
        {
            Set(PageSettings.LeftMenu, "ShopUserSettingsMenu");

            Add<Modules.ShopUserOrderList>();
        }
    }
}