using MSharp;
using Olive;

namespace Admin.Dashboard
{
    class SupportTicketResponsePage : RootPage
    {
        public SupportTicketResponsePage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Layout(Layouts.FrontEndModal);
            Add<Modules.SupportTicketResponse>();
        }
    }
}