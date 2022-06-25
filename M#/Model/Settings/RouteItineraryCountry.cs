using MSharp;

namespace Domain
{
    class RouteItineraryCountry : EntityType
    {
        public RouteItineraryCountry()
        {
            Associate<RouteItinerary>("RouteItinerary");
            Associate<Route>("Route");

            Associate<Country>("Country");
            SortableByOrder();
        }
    }
}