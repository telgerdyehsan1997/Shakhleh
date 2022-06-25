namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.AEB.DTOs;
    using Newtonsoft.Json;
    using Olive;
    using Olive.Entities;

    public class AEBIntegrationService
    {
        IDatabase Database;
        string API_AUTH_TOKEN;

        public AEBIntegrationService(IDatabase database)
        {
            Database = database;
            API_AUTH_TOKEN = GetAuthenticationToken().GetAwaiter().GetResult();
        }

        public async Task<string> Process(Shipment shipment)
        {
            // ToDo: 
            //var consignments = (await advice.Consignments.GetList());
            //consignments.Do(c => Process(c));
            var jsonData = "";

            foreach (var consignment in (await shipment.Consignments.GetList().ToList()))
            {
                jsonData += await Process(consignment);
            }

            return jsonData;

            //using (var client = new WebClient())
            //{
            //    var result = await client.UploadString(requestUrl, data);

            //    return result;
            //}

            // ToDo: Run validations for pre-advise

            // ToDo: Update pre-advice status
        }

        async Task<string> Process(Consignment consignment)
        {
            return new ConsignmentRequestDTO
            {
                ClientSystemId = Config.Get<string>("AEBCustomsManagement:ClientName"),
                ClientIdentCode = Config.Get<string>("AEBCustomsManagement:ClientCode"),
                UserName = Config.Get<string>("AEBCustomsManagement:UserName"),
                ResultLanguageIsoCodes = new List<string> { "en" },
                Consignment = consignment.ToDto()
            }.ToJson();
        }

        /// <summary>
        /// Authenticate and return the authentication token.
        /// </summary>
        async Task<string> GetAuthenticationToken()
        {
            var data = new AuthenticationRequestDTO
            {
                ClientName = Config.Get<string>("AEBCustomsManagement:ClientName"),
                UserName = Config.Get<string>("AEBCustomsManagement:UserName"),
                Password = Config.Get<string>("AEBCustomsManagement:Password"),
                LocaleName = "en",
                IsExternalLogon = true
            };

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                        Config.Get<string>("AEBCustomsManagement:AuthenticationApiUri"),
                        new StringContent(data.ToJson(), Encoding.UTF8, "application/json"));


                var responseContent = await (response.Content.ReadAsStringAsync());

                dynamic authToken = JsonConvert.DeserializeObject(responseContent);

                return authToken.token;
            }
        }
    }
}