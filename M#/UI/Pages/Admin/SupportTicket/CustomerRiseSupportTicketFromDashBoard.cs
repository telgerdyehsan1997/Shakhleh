using MSharp;

namespace Admin.Dashboard
{
    class CustomerRiseSupportTicketFromDashBoardPage : RootPage
    {
        public CustomerRiseSupportTicketFromDashBoardPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.CustomerRiseSupportTicketFromDashBoard>();
        }
    }
}