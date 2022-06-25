using MSharp;

namespace Admin.Dashboard
{
    class CustomerRiseSupportTicketPage : RootPage
    {
        public CustomerRiseSupportTicketPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.CustomerRiseSupportTicket>();
        }
    }
}