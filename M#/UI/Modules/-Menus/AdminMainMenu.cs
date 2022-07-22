using MSharp;
using Domain;

namespace Modules
{
    public class AdminMainMenu : MenuModule
    {
        public AdminMainMenu()
        {
            AjaxRedirect();
            WrapInForm(false);
            IsViewComponent();
            UlCssClass("nav");

            Item("تنظیمات")
                .CssClass("nav-item")
                .Go<Admin.SettingsPage>();

            Item("مغازه ها")
                .CssClass("nav-item")
                .Go<Admin.FoodShopsPage>();
        }
    }
}