using MSharp;
using Olive;

namespace Admin.Dashboard
{
    class SupportTicketListPage : RootPage
    {
        public SupportTicketListPage()
        {
            Roles(AppRole.Admin);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Add<Modules.SupportTicketList>();
        }
    }
}