using MSharp;

namespace Admin.Settings
{
    class RoutesPage : SubPage<SettingsPage>
    {
        public RoutesPage()
        {
            Add<Modules.RoutesList>();
        }
    }
}
