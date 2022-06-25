namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class ExchangeRate
    {
        public override string ToString(string format)
        {
            if (format == "F")
            {
                return $"{Country_Territory} {Currency.Name}";
            }
            return string.Format("{0}", Currency.Name);
        }
        public static Task<IEnumerable<ExchangeRate>> GetExchangeRateList(ExchangeRateFile exchangeRateFile)
        {
            return Database.Of<ExchangeRate>()
                .Where(item => item.FileId == exchangeRateFile)
                .OrderBy(x => x.Country_Territory).GetList();
        }
    }

}