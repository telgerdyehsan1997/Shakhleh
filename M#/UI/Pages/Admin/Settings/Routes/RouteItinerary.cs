using MSharp;

namespace Admin.Settings.Routes
{
    class RouteItineraryPage : SubPage<RoutesPage>
    {
        public RouteItineraryPage()
        {
            Add<Modules.RouteItineraryList>();
        }
    }
}
