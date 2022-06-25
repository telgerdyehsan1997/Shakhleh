using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.AEB.DTOs
{
    public class BaseDTO
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
                                               new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}