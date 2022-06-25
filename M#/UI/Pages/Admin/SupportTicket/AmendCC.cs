using MSharp;

namespace Admin.Dashboard
{
    class AmendCCPage : RootPage
    {
        public AmendCCPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.AmendCC>();
        }
    }
}