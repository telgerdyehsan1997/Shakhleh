using MSharp;
using Domain;

namespace Modules
{
    public class AdminSettingsMenu : MenuModule
    {
        public AdminSettingsMenu()
        {
            SubItemBehaviour(MenuSubItemBehaviour.ExpandCollapse);
            WrapInForm(false);
            AjaxRedirect();
            IsViewComponent();
            RootCssClass("sidebar-menu");
            UlCssClass("nav flex-column");
            Using("Olive.Security");

            Item("ادمین ها")
                .OnClick(x => x.Go<Admin.Settings.AdministratorsPage>());

            Item("مغازه ها")
                .OnClick(x => x.Go<Admin.Shops.ShopsPage>());

            Item("تنظیمات عمومی")
                .OnClick(x => x.Go<Admin.Settings.GeneralPage>());

        }
    }
}