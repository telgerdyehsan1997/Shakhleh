using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler
{
    public class GVMSSystem
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("isUnaccompanied")]
        public bool IsUnaccompanied { get; set; }

        [JsonProperty("vehicleRegNum")]
        public string VehicleRegNum { get; set; }

        [JsonProperty("trailerRegistrationNums", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TrailerRegistrationNums { get; set; }

        [JsonProperty("containerReferenceNums")]
        public List<string> ContainerReferenceNums { get; set; }

        [JsonProperty("plannedCrossing")]
        public PlannedCrossing PlannedCrossing { get; set; }

        [JsonProperty("customsDeclarations")]
        public List<CustomsDeclaration> CustomsDeclarations { get; set; }

        [JsonProperty("transitDeclarations")]
        public List<TransitDeclaration> TransitDeclarations { get; set; }
    }
}
