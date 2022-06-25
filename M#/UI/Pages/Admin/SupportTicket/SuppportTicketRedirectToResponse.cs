using MSharp;

namespace Admin.Dashboard
{
    class SuppportTicketRedirectToResponsePage : RootPage
    {
        public SuppportTicketRedirectToResponsePage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.SuppportTicketRedirectToResponse>();
        }
    }
}