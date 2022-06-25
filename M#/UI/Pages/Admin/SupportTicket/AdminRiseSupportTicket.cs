using MSharp;

namespace Admin.Dashboard
{
    class AdminRiseSupportTicketPage : RootPage
    {
        public AdminRiseSupportTicketPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
          //  Layout(Layouts.FrontEndModal);
            Add<Modules.AdminRiseSupportTicket>();
        }
    }
}