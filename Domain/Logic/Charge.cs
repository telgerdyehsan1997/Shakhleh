namespace Domain
{
   using System;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Collections.Generic;
   using Olive;
   using Olive.Entities;
   
   partial class Charge
   {
        protected override Task OnValidating(EventArgs e)
        {
            return base.OnValidating(e);
        }

        public override Task Validate()
        {
            if (this.ValidFrom?.Date.Day != 1 && this.ValidFrom.HasValue)
                throw new ValidationException("Valid From must start from first day of the month.");

            if (this.ValidFrom?.Date < LocalTime.Today && this.ValidFrom.HasValue)
                throw new ValidationException("Valid From must be in the future.");

            return base.Validate();
        }
        public static Task<IEnumerable<Charge>> GetChargeList()
        {
            return Database.Of<Charge>().Where(x => x.Name != "Custom" && x.IsDefault).GetList();

        }
    }
}