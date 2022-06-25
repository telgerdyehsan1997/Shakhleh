namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Text.RegularExpressions;

    partial class AuthorisedLocation
    {
        public override async Task Validate()
        {
            await base.Validate();

            if (GetEmailAddresses().Any(x => !Regex.IsMatch(x, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")))
                throw new ValidationException("Email addresses provided are invalid. Email addresses should have valid format and be comma separated.");
        }

        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            var anyCompanyUsingAuthorisedLocation = await Database.Any<CompanyAuthorisedLocationsLink>(a => a.AuthorisedlocationId == this.ID);

            if (IsDeactivated && anyCompanyUsingAuthorisedLocation)
                throw new ValidationException("Cannot archive this AuthorisedLocation. At least one company is using this AuthorisedLocation");

            EmailAddresses = GetEmailAddresses().ToString(",");
        }

        public IEnumerable<string> GetEmailAddresses()
            => EmailAddresses?.Split(',').Trim().Select(x => x.Trim()) ?? Enumerable.Empty<string>();

        protected override async Task OnSaved(SaveEventArgs e)
        {
            if (!await Database.Any<GuaranteeLength>(x => x.AuthorisedLocationId == ID))
            {
                try
                {
                    await Database.Save(new GuaranteeLength
                    {
                        Length = 8,
                        AuthorisedLocationId = ID
                    });
                }
                catch (Exception)
                {
                    throw;
                }
            }
            await base.OnSaved(e);
        }
    }

}