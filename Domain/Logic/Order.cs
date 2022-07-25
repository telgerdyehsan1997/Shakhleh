namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Security;
    using Olive.Web;

    partial class Order
    {
        decimal? GetTotalPrice() => FoodItems.GetList().GetAwaiter().GetResult().Sum(x=>x.Food.Price *x.Count);

    }
}