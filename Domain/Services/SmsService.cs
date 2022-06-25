using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IO.ClickSend.ClickSend.Api;
using IO.ClickSend.Client;
using IO.ClickSend.ClickSend.Model;
using Newtonsoft.Json;
using Olive;
using Olive.Entities.Data;
using Olive.Entities;

namespace Domain
{
    public class SmsService : ISmsService
    {
        SMSApi ApiInstance;
        IDatabase Database;
        public SmsService(IConfiguration configuration, IDatabase database)
        {
            Database = database;
            var config = new Configuration()
            {
                Username = configuration.GetValue<string>("SMS:Api:Username"),
                Password = configuration.GetValue<string>("SMS:Api:Key")
            };

            ApiInstance = new SMSApi(config);
        }

        public async Task<MFAStatus> Dispatch(MFAMessage sms)
        {
            var body = $"Your ChannelPorts authentication code is {sms.MFAKey}";

            var listOfSms = new List<SmsMessage>
            {
                new SmsMessage(
                    to: sms.To,
                    body: body
                )
            };
            try
            {
                var smsCollection = new SmsMessageCollection(listOfSms);
                var response = await ApiInstance.SmsSendPostAsync(smsCollection);
                var result = JsonConvert.DeserializeObject<SmsResponse>(response);

                await Database.Update(sms, t =>
                {
                    t.ResponseCode = result.http_code.ToString();
                    t.Sent = true;
                });
                const int code = 200;
                return result.http_code == code ? MFAStatus.Sent : MFAStatus.Faield;
            }
            catch (Exception ex)
            {
                Log.For<MFAMessage>().Error(ex);
                return MFAStatus.Faield;
            }
        }

    }

    public class SmsResponse
    {
        public int http_code { get; set; }

        public string response_code { get; set; }
    }
}
