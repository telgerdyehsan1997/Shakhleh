using MSharp;
using Domain;

namespace Modules
{
    public class DashboardMenu : MenuModule
    {
        public DashboardMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");

        }
    }
}