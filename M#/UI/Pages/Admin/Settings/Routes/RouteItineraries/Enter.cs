using MSharp;

namespace Admin.Settings.Routes.RouteItineraries
{
    class EnterPage : SubPage<RouteItineraryPage>
    {
        public EnterPage()
        {
            Add<Modules.RouteItineraryForm>();
        }

    }
}
