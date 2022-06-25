using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class CurrencyExchangeService
    {
        static IDatabase Database => Context.Current.Database();

        public static async Task<decimal> ExchangeToGBP(Currency currency, decimal value, DateTime shipmentDate)
        {
            var exchange = await Database.Of<ExchangeRate>()
                            .Where(t => t.CurrencyId == currency && t.From <= shipmentDate.Date && t.To >= shipmentDate.Date)
                            .GetList().FirstOrDefault();

            if (exchange == null)
                throw new ValidationException("Exchange rate service failed to locate any exchange rate.");

            var rate = exchange.Rate;

            var result = value / rate;
            return result.RoundUpWithDecimalPlaces(2);
        }

        public static Task<ExchangeRate> GetTerritory(Currency currency)
        {
            return Database.Of<ExchangeRate>().Where(t => t.CurrencyId == currency)
                .OrderByDescending(t => t.To)
                .FirstOrDefault();
        }
    }
}
