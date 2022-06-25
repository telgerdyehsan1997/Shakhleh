using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Olive;

namespace Domain
{
    partial class Route
    {

        private static readonly Dictionary<string, string> ROUTE_MAP = new Dictionary<string, string> {
            {"DEU", "CQF"},
            {"DOV", "CQF"},
            {"FXT", "RTM"},
            {"HLD", "DUB"},
            {"HRH", "RTM"},
            {"HUL", "RTM"},
            {"IMM", "ROT"},
            {"KIL", "RTM"},
            {"LIV", "DUB"},
            {"LON", "ZEE"},
            {"MED", "CQF"},
            {"MID", "RTM"},
            {"MIL", "ROE"},
            {"NHV", "DPE"},
            {"POO", "CER"},
            {"PTM", "CFR"},
            {"PUF", "ZEE"},
            {"TYN", "IJM"}
        };

        public static async Task<Route> GetRoute(string ukPortCode, string nonUkportCode)
        {
            var ukPort = await Port.GetActivePortByCode(ukPortCode);
            var nonUKPort = await Port.GetActivePortByCode(nonUkportCode);

            var route = await Database.FirstOrDefault<Route>(x => x.UKPortId == ukPort
                                                               && x.Non_UKPortId == nonUKPort
                                                               && !x.IsDeactivated);

            if (route == null)
                throw new ValidationException("Invalid route information or route does not exist.");

            return route;
        }

        public static async Task<Route> GetRoute(Port port)
        {
            var nonUK = string.Empty;
            Port nonUKPort = null;
            Route route = null;

            if (ROUTE_MAP.TryGetValue(port.PortCode, out nonUK))
                nonUKPort = await Database.FirstOrDefault<Port>(x => x.PortCode == nonUK && x.Non_UK == true && !x.IsDeactivated);

            if (nonUKPort != null)
                route = await Database.FirstOrDefault<Route>(x => x.UKPortId == port && x.Non_UKPortId == nonUKPort
                                                             && !x.IsDeactivated);
            else
                route = await Database.FirstOrDefault<Route>(x => x.UKPortId == port
                                                                && !x.IsDeactivated);
            if (route == null)
                throw new ValidationException("Invalid route information or route does not exist.");

            return route;
        }



        public async Task SetDefaultItinerariy()
        {
            foreach (var item in await RouteItineraryCountry.GetList())
            {
                var routeItinerary = await this.RouteItinerary.FirstOrDefault();
                await Database.Update(item, x => x.RouteItinerary = routeItinerary);
            }
        }


        public async static Task<IEnumerable<Country>> GetItineries(Consignment consignment)
        {
            var shipment = consignment.Shipment;
            var commodityCountry = (await consignment.Commodities.FirstOrDefault()).CountryOfDestination;
            var countries = new List<Country>();

            if (shipment.RouteId.HasValue)
            {
                var route = shipment.Route;
                var iternatries = await route.RouteItinerary.Where(x => x.DestinationCountryId.HasValue && x.DestinationCountry.Code == commodityCountry.Code).FirstOrDefault();

                if (iternatries != null)
                {
                    var nonUKcountry = route.Non_UKPort.Country.Code.ToUpper();
                    var ukPort = route.UKPort.Country.Code.ToUpper();
                    var destinationCountry = iternatries.DestinationCountry.Code.ToUpper();

                    if (destinationCountry != nonUKcountry)
                        countries.Add(shipment.Route.Non_UKPort?.Country);

                    if ((!shipment.IsInUK && destinationCountry != nonUKcountry) || (shipment.IsInUK && destinationCountry != ukPort))
                    {
                        countries.AddRange(await iternatries.RouteItineraryCountry.OrderBy(x => x.Order).GetList().Select(x => x.Country));
                    }
                }
                else
                {
                    countries.AddRange(await route.RouteItineraryCountry.Where(x => x.RouteItinerary.HasDefault).OrderBy(x => x.Order).GetList().Select(x => x.Country));
                }
            }

            return countries.ExceptNull().ToList();
        }


        public async static Task<IEnumerable<Country>> GetItineriesForICS(Consignment consignment)
        {
            var shipment = consignment.Shipment;
            var commodityCountry = (await consignment.Commodities.FirstOrDefault()).CountryOfDestination;
            var countries = new List<Country>();
            var selectedCountry = new List<Country>();

            if (shipment.RouteId.HasValue)
            {
                var route = shipment.Route;
                var iternatries = await route.RouteItinerary.Where(x => x.DestinationCountryId == commodityCountry).FirstOrDefault();
                var hasDesination = false;

                if (iternatries != null)
                {
                    hasDesination = true;
                    selectedCountry = await iternatries.RouteItineraryCountry.OrderByDescending(x => x.Order).GetList().Select(x => x.Country).ToList();
                }
                else
                    selectedCountry = await route.RouteItineraryCountry.Where(x => x.RouteItinerary.HasDefault).OrderByDescending(x => x.Order).GetList().Select(x => x.Country).ToList();


                if (hasDesination)
                    countries.Add(commodityCountry);

                countries.AddRange(selectedCountry);
                countries.Add(shipment.Route.Non_UKPort?.Country); // country 2
                countries.Add(shipment.Route.UKPort?.Country); // country 1
            }

            return countries.ExceptNull().ToList();
        }

    }
}
