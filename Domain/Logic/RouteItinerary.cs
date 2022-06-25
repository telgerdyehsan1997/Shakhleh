namespace Domain
{
   using System;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Collections.Generic;
   using Olive;
   using Olive.Entities;
   
   partial class RouteItinerary
   {
        public static Task<RouteItinerary> FindByDesinationCountry(Route route, Country destinationCountry)
        {
            return Database.FirstOrDefault<RouteItinerary>(x => x.RouteId == route && x.DestinationCountryId == destinationCountry);
        }

        public static Task<RouteItinerary> FindByRouteDetfault(Route route)
        {
            return Database.FirstOrDefault<RouteItinerary>(x => x.RouteId == route && x.HasDefault);
        }

        [Calculated]
        public IEnumerable<string> ImportCountries { get; set; }
   }

}