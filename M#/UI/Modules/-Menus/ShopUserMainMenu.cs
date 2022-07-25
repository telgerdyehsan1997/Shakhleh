using MSharp;
using Domain;

namespace Modules
{
    public class ShopUserMainMenu : MenuModule
    {
        public ShopUserMainMenu()
        {
            AjaxRedirect();
            WrapInForm(false);
            IsViewComponent();
            UlCssClass("nav");

            Item("تنظیمات")
                .CssClass("nav-item")
                .Go<ShopUser.SettingsPage>();

            Item("مشتریان")
                .CssClass("nav-item")
                .Go<ShopUser.CustomersPage>();

            Item("سفارشات")
                .CssClass("nav-item")
                .Go<ShopUser.OrdersPage>();

        }
    }
}