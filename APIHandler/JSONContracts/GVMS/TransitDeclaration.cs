using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler
{
    public class TransitDeclaration
    {
        [JsonProperty("transitDeclarationId")]
        public string TransitDeclarationId { get; set; }

        [JsonProperty("isTSAD")]
        public bool IsTSAD { get; set; }
    }
}
