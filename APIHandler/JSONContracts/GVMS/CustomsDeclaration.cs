using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler
{
    public class CustomsDeclaration
    {
        [JsonProperty("customsDeclarationId")]
        public string CustomsDeclarationId { get; set; }

        [JsonProperty("sAndSMasterRefNum", NullValueHandling = NullValueHandling.Ignore)]
        public string SAndSMasterRefNum { get; set; }
    }
}
