namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    using Olive.Security;
    using Olive.Web;

    partial class Order
    {
        decimal? GetTotalPrice() => FoodItems.GetList().GetAwaiter().GetResult().Sum(x => x.Food.Price * x.Count);

        public async Task ApplyDiscount(Discount discount)
        {
            if (discount == null)
                return;

            //if (discount.MinimumAmountOfPriceToUse.HasValue &&
            //    !discount.IsFoodSpecific &&
            //    discount.MinimumAmountOfPriceToUse > order.TotalPrice)
            //    return;

            //if (discount.CalculationType == DiscountCalculationType.Amount)
            //{

            //}

            if (discount.CalculationTypeId == DiscountCalculationType.Percentage.ID)
            {
                var totalPriceWithDiscount = await GetDiscountedPrice(discount);
                await Database.Update(this, x => {
                    x.TotalPriceWithDiscount = totalPriceWithDiscount;
                }, SaveBehaviour.BypassAll);
            }
        }

        public async Task<decimal?> GetDiscountedPrice(Discount discount)
            => await (await FoodItems.GetList()).Select(x => x.Food)
            .Sum(async x=>await x.GetDiscountedPrice(discount,this));
    }

}