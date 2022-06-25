using MSharp;

namespace Admin.Dashboard
{
    class ShipmentFileErrorLogPage : RootPage
    {
        public ShipmentFileErrorLogPage()
        {
            BrowserTitle("Shipments > View");
            Roles(AppRole.Admin);
            Set(PageSettings.LeftMenu, "DashboardMenu");
            Add<Modules.ShipmentFileErrorLogList>();

        }
    }
}