namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class Shop
    {
        public async Task<Discount> GetActiveDiscount() =>
            (await Discounts.Where(x => x.Start <= LocalTime.Today && x.End >= LocalTime.Today)
            .GetList()).FirstOrDefault();
    }

}