using MSharp;

namespace Admin.Dashboard
{
    class SuppportTicketReOpenPage : RootPage
    {
        public SuppportTicketReOpenPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.SuppportTicketReOpen>();
        }
    }
}