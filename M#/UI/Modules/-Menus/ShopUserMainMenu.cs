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

            Item("سلااااااااااااااام")
                .CssClass("nav-item")
                .Go<ShopUser.SettingsPage>();

        }
    }
}