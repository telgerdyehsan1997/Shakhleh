using MSharp;
using Domain;

namespace Modules
{
    public class AdminFoodShopsMenu : MenuModule
    {
        public AdminFoodShopsMenu()
        {
            SubItemBehaviour(MenuSubItemBehaviour.ExpandCollapse);
            WrapInForm(false);
            AjaxRedirect();
            IsViewComponent();
            RootCssClass("sidebar-menu");
            UlCssClass("nav flex-column");
            Using("Olive.Security");

            Item("مشخصات")
                .OnClick(x => x.Go<Admin.FoodShops.DetailsPage>());
        }
    }
}