using MSharp;

namespace Domain
{
    class RouteItinerary : EntityType
    {
        public RouteItinerary()
        {
            this.Archivable();
            Bool("Has default").Mandatory();
            Associate<Route>("Route").DatabaseIndex();
            Associate<Country>("Destination country").DatabaseIndex();
            Associate<Country>("UK country").DatabaseIndex();
            Associate<Country>("Non UK country").DatabaseIndex();
            InverseAssociate<RouteItineraryCountry>("RouteItineraryCountry", "RouteItinerary");
        }
    }
}