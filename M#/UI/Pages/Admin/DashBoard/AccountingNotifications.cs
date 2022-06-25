using MSharp;

namespace Admin.Dashboard
{
    class AccountingNotificationsPage : RootPage
    {
        public AccountingNotificationsPage()
        {
            BrowserTitle("Shipments > View");
            Roles(AppRole.Admin);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Add<Modules.AccountingNotificationsList>();

        }
    }
}