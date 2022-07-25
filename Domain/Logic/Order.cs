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
                var discountedRate = (100 - discount.Percent) / 100;
                var TotalPriceWithDiscount = TotalPrice * discountedRate;
                await Database.Update(this, x => {
                    x.TotalPriceWithDiscount = TotalPriceWithDiscount;
                }, SaveBehaviour.BypassAll);
            }
        }
    }

}