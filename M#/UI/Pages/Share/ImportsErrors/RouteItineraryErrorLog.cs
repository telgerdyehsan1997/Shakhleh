using MSharp;

namespace Admin.Settings.Import
{
    class RouteItineraryErrorLogPage : SubPage<Admin.Settings.RoutingItinerariesImportPage>
    {
        public RouteItineraryErrorLogPage()
        {
            Add<Modules.RouteItineraryImportErrorList>();
        }
    }
}