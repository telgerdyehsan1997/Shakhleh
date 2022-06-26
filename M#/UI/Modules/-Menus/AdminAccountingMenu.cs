using MSharp;

namespace Modules
{
    class AdminAccountingMenu : MenuModule
    {
        public AdminAccountingMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");

        }
    }
}