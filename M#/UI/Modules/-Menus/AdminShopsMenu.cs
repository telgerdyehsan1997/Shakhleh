using MSharp;
using Domain;

namespace Modules
{
    public class AdminShopsMenu : MenuModule
    {
        public AdminShopsMenu()
        {
            SubItemBehaviour(MenuSubItemBehaviour.ExpandCollapse);
            WrapInForm(false);
            AjaxRedirect();
            IsViewComponent();
            RootCssClass("sidebar-menu");
            UlCssClass("nav flex-column");
            Using("Olive.Security");

            Item("مشخصات")
                .OnClick(x => x.Go<Admin.Shops.OverviewPage>());
        }
    }
}