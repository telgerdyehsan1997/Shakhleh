using MSharp;
using Domain;

namespace Modules
{
    public class AdminSettingsMenu : MenuModule
    {
        public AdminSettingsMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");


            //Item("Row Quotas Imports")
            //    .OnClick(x => x.Go<Admin.Settings.RowQuotaImportsPage>());
        }
    }
}