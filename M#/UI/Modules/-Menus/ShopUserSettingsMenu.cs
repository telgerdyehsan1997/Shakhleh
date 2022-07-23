using MSharp;
using Domain;

namespace Modules
{
    public class ShopUserSettingsMenu : MenuModule
    {
        public ShopUserSettingsMenu()
        {
            SubItemBehaviour(MenuSubItemBehaviour.ExpandCollapse);
            WrapInForm(false);
            AjaxRedirect();
            IsViewComponent();
            RootCssClass("sidebar-menu");
            UlCssClass("nav flex-column");
            Using("Olive.Security");

            Item("غذاها")
                .OnClick(x => x.Go<ShopUser.Settings.FoodsPage>());

            ViewModelProperty<Domain.Shop>("Shop");

            OnPreBound("Binding Shop")
                .Code("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

        }
        
    }
}