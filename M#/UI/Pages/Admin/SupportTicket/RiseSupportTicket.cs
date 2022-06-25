using MSharp;

namespace Admin.Dashboard
{
    class RiseSupportTicketPage : RootPage
    {
        public RiseSupportTicketPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.RiseSupportTicket>();
        }
    }
}