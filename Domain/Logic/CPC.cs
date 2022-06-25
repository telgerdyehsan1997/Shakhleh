namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Security;
    using Olive.Entities;

    /// <summary> 
    /// Provides the business logic for Country class.
    /// </summary>
    partial class CPC
    {
        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            var anyCountriesUsingCPC = await Database.GetList<Country>().Any(c => Task.Factory.RunSync(() => c.IsUsingCPC(this)));
            var anyCompanyUsingCPC = await Database.GetList<Company>().Any(c => Task.Factory.RunSync(() => c.IsUsingCPC(this)));

            if (IsDeactivated && anyCountriesUsingCPC)
                throw new ValidationException("Cannot archive this CPC. At least one country is using this CPC for importing/exporting.");
            if (IsDeactivated && anyCompanyUsingCPC)
                throw new ValidationException("Cannot archive this CPC. At least one company is using this CPC for Special CPCs");
        }
    }
}
