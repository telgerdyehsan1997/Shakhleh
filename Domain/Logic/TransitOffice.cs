namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Text.RegularExpressions;

    partial class TransitOffice
    {
        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            if (!Validator.IsValidNCTS(NCTSCode))
                throw new ValidationException("The 8 digit Reference code must have first 2 characters alpha only, and the last 6 characters alphanumeric.");

            if (!(Departure == true || Destination == true || Transit == true))
                throw new ValidationException("At least one of the destination, departure or transit flags must be checked.");
        }

        public static Task<IEnumerable<TransitOffice>> Departures => Database.GetList<TransitOffice>(x => x.Departure == true);

        public static Task<IEnumerable<TransitOffice>> Transits => Database.GetList<TransitOffice>(x => x.Transit == true);
        public static Task<IEnumerable<TransitOffice>> Destinations => Database.GetList<TransitOffice>(x => x.Destination == true && !x.IsDeactivated);
        public static Task<IEnumerable<TransitOffice>> NonGBDestinations => Database.GetList<TransitOffice>(x => x.Destination == true && x.CountryCode != "GB" && !x.IsDeactivated);


        public static Task<TransitOffice> FindByCodeAndName(string code, string name)
        {
            return Transits.FirstOrDefault(c => c.CountryCode == code && c.CountryName == name);
        }
    }
}