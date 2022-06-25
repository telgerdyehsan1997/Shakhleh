using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler
{
    public class PlannedCrossing
    {
        [JsonProperty("routeId")]
        public string RouteId { get; set; }

        [JsonProperty("localDateTimeOfDeparture")]
        public string LocalDateTimeOfDeparture { get; set; }
    }
}
