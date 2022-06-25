using MSharp;
using Domain;

namespace Modules
{
    public class DashboardMenu : MenuModule
    {
        public DashboardMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");

            Item("Accounting Notifications")
               .OnClick(x => x.Go<Admin.Dashboard.AccountingNotificationsPage>());

            Item("Error Notifications")
                .OnClick(x => x.Go<Admin.Dashboard.ShipmentFileErrorLogPage>());

            Item("Support Tickets")
                .OnClick(x => x.Go<Admin.Dashboard.SupportTicketListPage>());
        }
    }
}