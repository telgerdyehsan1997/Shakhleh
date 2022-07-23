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
                .OnClick(x => x.Go<Admin.Shops.OverviewPage>().Send("shop", "info.Shop.ID"));

            Item("غذاها")
                .OnClick(x => x.Go<Admin.Shops.FoodsPage>().Send("shop", "info.Shop.ID"));

            ViewModelProperty<Domain.Shop>("Shop").FromRequestParam("shop");

        }
    }
}