using MSharp;
using Domain;

namespace Modules
{
    public class CustomerMainMenu : MenuModule
    {
        public CustomerMainMenu()
        {
            AjaxRedirect();
            WrapInForm(false);
            IsViewComponent();
            UlCssClass("nav");

            Item("سلااااااااااااااام")
                .CssClass("nav-item")
                .Go<Admin.SettingsPage>();

        }
    }
}