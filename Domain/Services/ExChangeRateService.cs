using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Net;
using Olive.Entities.Data;
using Olive.Entities;
using System.Net.Http;
using System.Xml;

namespace Domain
{
    public class ExChangeRateService : IExChangeRateService
    {
        IDatabase Database;
        public ExChangeRateService(IDatabase database)
        {
            Database = database;
        }
        public async Task GetExChangeRate()
        {
            try
            {
                var files = await Database.Of<ExchangeRateFile>().Where(x => x.URL == AppSetting.ExchangeRateUrl).FirstOrDefault();
                if (files != null) return;
                using (var scope = Database.CreateTransactionScope())
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(AppSetting.ExchangeRateUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode || resp.IsEmpty())
                            return;

                        var xml = new XmlDocument();
                        xml.LoadXml(resp);
                        var responses = xml.SelectNodes(".//exchangeRate");
                        var dates = xml.DocumentElement.GetAttribute("Period")?.Split("to");
                        var exchangesFile = new ExchangeRateFile
                        {
                            Name = AppSetting.ExchangeRateUrl.Split("/").Last(),
                            SubmitDate = LocalTime.Now,
                            URL = AppSetting.ExchangeRateUrl,
                        };
                        exchangesFile = await Database.Save(exchangesFile);
                        foreach (var responseNode in responses)
                        {
                            var node = (XmlNode)responseNode;
                            var countryName = node.SelectSingleNode(".//countryName").InnerText;
                            var countryCode = node.SelectSingleNode(".//countryCode").InnerText;
                            var currencyName = node.SelectSingleNode(".//currencyName").InnerText;
                            var currencyCode = node.SelectSingleNode(".//currencyCode").InnerText;
                            var rateNew = node.SelectSingleNode(".//rateNew").InnerText;
                            var currency = await Database.Of<Currency>().Where(x => x.Name == currencyCode).FirstOrDefault();
                            if (currency == null)
                            {
                                currency = await Database.Save(new Currency
                                {
                                    Name = currencyCode,
                                });
                            }
                            await Database.Save(new ExchangeRate
                            {
                                Currency = currency,
                                Rate = rateNew.To<decimal>(),
                                File = exchangesFile,
                                Country_Territory = countryName,
                                From = dates[0].To<DateTime>(),
                                To = dates[1].To<DateTime>(),

                            });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Log.For<ExchangeRateFile>().Error(ex);
            }
        }
    }
}
