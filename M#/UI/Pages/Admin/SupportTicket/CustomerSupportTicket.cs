using MSharp;
using Olive;

namespace Admin.Dashboard
{
    class CustomerSupportTicketPage : RootPage
    {
        public CustomerSupportTicketPage()
        {
            Roles(AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Add<Modules.CustomerSupportTicketList>();
         
        }
    }
}