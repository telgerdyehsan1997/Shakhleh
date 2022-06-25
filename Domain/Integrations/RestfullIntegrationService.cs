using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RestfullIntegrationService
    {
        RestClient Client;
        JWTProviderType Type;
        public RestfullIntegrationService(JWTProviderType type, string url)
        {
            Type = type;
            Client = new RestClient(url);
        }

        public async Task<RestResponse> Post(object obj)
        {
            var token = new JWTProvider().Of(Type).GenerateToken();
            var body = JsonConvert.SerializeObject(obj);

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}

            var request = new RestRequest(Method.POST);
            request.Parameters.Clear();
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return await GetResponseContentAsync(Client, request) as RestResponse;
        }

        private Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
           {
               tcs.SetResult(response);
           });

            return tcs.Task;
        }
    }
}
