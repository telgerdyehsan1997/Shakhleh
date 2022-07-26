namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class Food
    {
        private int? _discountedPrice;

        public async Task<int?> GetDiscountedPrice(Discount discount, Order order)
        {
            if (_discountedPrice.HasValue) return _discountedPrice;

            if (discount.FoodTypeId.IsAnyOf(DiscountFoodType.AllFoodsButThereIsExclusion.ID))
                if (discount.CachedExcludedFoods.Any(x => x.ID == ID))
                    return Price.Value;
            
            if (discount.FoodTypeId.IsAnyOf(DiscountFoodType.OnlySpecifiedFoods.ID))
                if (discount.CachedDiscountedFoods.None(x => x.ID == ID))
                    return Price.Value;

            if (discount.End.HasValue && discount.End.Value < LocalTime.Today)
                return Price.Value;

            if (discount.Start.HasValue && discount.Start.Value > LocalTime.Today)
                return Price.Value;


            //if (discount.CalculationTypeId.IsAnyOf(DiscountCalculationType.Amount))
            //    return Price.Value - discou


            if (discount.CalculationTypeId.IsAnyOf(DiscountCalculationType.Percentage)) 
            {
                var discountedRate = (100 - discount.Percent.Value) / 100;
                _discountedPrice = (int)(Price.Value * discountedRate);
                return _discountedPrice;
            }

            return Price.Value;
        }
    }

}