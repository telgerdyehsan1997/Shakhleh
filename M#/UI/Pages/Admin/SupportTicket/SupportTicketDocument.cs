using MSharp;

namespace Admin.Dashboard
{
    class SupportTicketDocumentPage : RootPage
    {
        public SupportTicketDocumentPage()
        {
            Roles(AppRole.Admin,AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Add<Modules.SupportTicketDocumentList>();
        }
    }
}