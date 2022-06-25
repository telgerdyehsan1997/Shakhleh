using MSharp;

namespace Admin.Settings
{
    class RoutingItinerariesImportPage : SubPage<SettingsPage>
    {
        public RoutingItinerariesImportPage()
        {
            Add<Modules.ImportRoutingItineraryList>();
        }
    }
}
