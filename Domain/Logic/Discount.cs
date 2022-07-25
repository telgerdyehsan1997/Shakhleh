namespace Domain
{
   using System;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Collections.Generic;
   using Olive;
   using Olive.Entities;
   
   partial class Discount
   {
        private List<Food> _excludedFoods;
        public List<Food> CachedExcludedFoods
        {
            get
            {
                if (_excludedFoods==null)
                {
                    _excludedFoods = ExcludedFoods.GetAwaiter().GetResult().ToList();
                }
                return _excludedFoods;
            }
        }
        private List<Food> _discountedFoods;
        public List<Food> CachedDiscountedFoods
        {
            get
            {
                if (_discountedFoods == null)
                {
                    _discountedFoods = ExcludedFoods.GetAwaiter().GetResult().ToList();
                }
                return _discountedFoods;
            }
        }
    }

}