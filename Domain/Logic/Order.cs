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
        int? GetTotalPrice() => FoodItems.GetList().GetAwaiter().GetResult().Sum(x => x.Food.Price * x.Count);

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

        public async Task<int?> GetDiscountedPrice(Discount discount)
            => await (await FoodItems.GetList())
            .Sum(async x=>(await x.Food.GetDiscountedPrice(discount, this))*x.Count);


        protected override Task OnSaving(CancelEventArgs e)
        {
            OrderDate = LocalTime.Now;
            return base.OnSaving(e);
        }
    }

}