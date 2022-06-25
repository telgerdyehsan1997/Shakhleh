using MSharp;

namespace Admin.Company
{
    class BoardcastMessagePage : SubPage<Admin.Dashboard.SupportTicketListPage>
    {
        public BoardcastMessagePage()
        {
            Add<Modules.BroadcastMessageForm>();
            BaseController("MFABaseController");
        }
    }
}