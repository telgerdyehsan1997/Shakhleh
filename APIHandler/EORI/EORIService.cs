using EuropaTaxation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Olive;
using System.Xml;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace APIHandler
{
    public interface IEORIService
    {
        bool IsEoriNumberValidate(string number);
        Task<bool> IsGBEoriNumberValidate(string number);
    }

    public class EORIService : IEORIService
    {
        string _url;

        public EORIService()
        {
            _url = Config.Get<string>("Integration:EORI:Url"); ;
        }

        public bool IsEoriNumberValidate(string number)
        {
            try
            {
                var soapEnvelopeXml = CreateSoapEnvelope(number);
                var webRequest = CreateWebRequest(_url);
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                var asyncResult = webRequest.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();

                string response;
                using (var webResponse = webRequest.EndGetResponse(asyncResult))
                using (var rd = new StreamReader(webResponse.GetResponseStream()))
                    response = rd.ReadToEnd();

                var startPos = response.IndexOf("<return>");
                var lastPos = response.LastIndexOf("</return>") - startPos + 9;
                var responseFormatted = response.Substring(startPos, lastPos);
                var serializer = new XmlSerializer(typeof(EoriResponseModel));
                EoriResponseModel result;

                using (TextReader reader = new StringReader(responseFormatted))
                    result = (EoriResponseModel)serializer.Deserialize(reader);

                return result.Result.All(t => t.Status == "0");
            }
            catch (Exception ex)
            {
                // #120422 - T7
                return true;
            }
        }

        private HttpWebRequest CreateWebRequest(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope(string number)
        {
            var soapEnvelopeDocument = new XmlDocument();
            var xmlBuilder = new StringBuilder();
            xmlBuilder.AppendFormat("<soap:Envelope xmlns:soap={0} >", "'http://schemas.xmlsoap.org/soap/envelope/'");
            xmlBuilder.Append("<soap:Body>");
            xmlBuilder.AppendFormat("<ev:validateEORI xmlns:ev={0} >", "'http://eori.ws.eos.dds.s/'");
            xmlBuilder.AppendFormat("<ev:eori>{0}</ev:eori>", number);
            xmlBuilder.Append("</ev:validateEORI>");
            xmlBuilder.Append("</soap:Body> ");
            xmlBuilder.Append("</soap:Envelope> ");

            soapEnvelopeDocument.LoadXml(xmlBuilder.ToString());
            return soapEnvelopeDocument;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (var stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        public async Task<bool> IsGBEoriNumberValidate(string number)
        {
            var result = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var baseUrl = Config.Get<string>("Integration:Gov:Base");
                    client.BaseAddress = new Uri(uriString: baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.hmrc.1.0+json"));
                    var eoris = "{\"eoris\":[\"" + number + "\"]}";
                    var temp = await client.PostAsync("customs/eori/lookup/check-multiple-eori", content: new StringContent(eoris, Encoding.UTF8, "application/json"));
                    var response = temp.Content.ReadAsStringAsync().Result;
                    var results = JsonConvert.DeserializeObject<List<EoriResponse>>(response);

                    if (results.First().valid)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            
            return result;
        }
    }


    public class EoriResponse
    {
        public string eori { get; set; }
        public bool valid { get; set; }
        public DateTime processingDate { get; set; }
    }

    public class Eoris
    {
        public List<eoriResponse> eoris { get; set; }
    }

}
