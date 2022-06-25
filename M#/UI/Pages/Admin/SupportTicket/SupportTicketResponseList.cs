using MSharp;
using Olive;

namespace Admin.Dashboard
{
    class SupportTicketResponseListPage : RootPage
    {
        public SupportTicketResponseListPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Add<Modules.SupportTicketResponseList>();
            OnStart(x =>
            {
                x.CSharp(@"await Database.Update(await Helper.TotalUnseenResponse(info.SupportTicket),x=>x.HasSeen = true);");
            });
        }
    }
}