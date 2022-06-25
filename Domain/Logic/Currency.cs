namespace Domain
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Security;
    using Olive.Entities;
    public partial class Currency
    {
        public string GetListDisplay()
        {
            if (this.Name == "GBP")
                return $"Great Britain - {Name}";
            var country = Task.Factory.RunSync(() => CurrencyExchangeService.GetTerritory(this))?.Country_Territory;
            if (country.HasValue())
                return $"{country} - {Name}";
            return Name;
        }
    }
}
