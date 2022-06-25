namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using System.Text;
    using APIHandler;
    using System.Linq.Expressions;

    partial class Carrier
    {
        IEORIService EORIService = Context.Current.GetService<IEORIService>();
        const int LENGTH = 20;

        public override async Task Validate()
        {
            await base.Validate();

            await ValidateFields();
        }

        async Task ValidateFields()
        {
            Town = Town.CapitaliseFirstLetters();

            if (Country.Code == "GB" && EORINumber.IsEmpty())
                throw new ValidationException("The EORI number field is required.");

            if (Country.Code == "GB" && (!EORINumber?.StartsWith("GB") ?? false))
                throw new ValidationException("The UK EORI number must start with GB.");

            var isValidEORI = await IsEORINumberValid(EORINumber, Country);

            if (EORINumber.HasValue() && !isValidEORI)
                throw new ValidationException("Carrier EORI number is invalid.");

            if (await IsCarrierUnique())
                throw new ValidationException("There is an existing Company with the same EORI number, Name, Postcode and Town/City in the database already.");

            if (!IsCreatedFromAPI)
            {
                CreatedBy = Context.Current.User().ExtractUser<User>();
                Company = (CreatedBy as CompanyUser)?.Company;
            }

            if (CompanyId != Constants.ChannelPortsID && EORINumber?.Equals(Constants.ChannelPortEORI) == true && !this.IsCreatedFromAPI)
                throw new ValidationException("You can't use this EORI.");

        }

        public string GetDisplayText()
        {
            var result = new StringBuilder();
            result.Append($"{Name} - {Town} - {Postcode}");

            if (EORINumber.HasValue())
                result.Append($" - {EORINumber}");

            return result.ToString();
        }
        public static async Task<IEnumerable<Carrier>> GetCarriers()
        {
            var user = Context.Current.User().ExtractUser<User>();
            if (user is CompanyUser)
                return await Database.Of<Carrier>().Where(x => !x.IsDeactivated && !x.IsCreatedFromAPI && x.CompanyId == (user as CompanyUser).CompanyId).GetList();
            else
                return await Database.Of<Carrier>().Where(x => !x.IsDeactivated && !x.IsCreatedFromAPI).GetList();

        }
        private async Task<bool> IsCarrierUnique()
        {
            if (EORINumber.HasValue() && !IsCreatedFromAPI)
            {
                if (await Database.Any<Carrier>(c => c != this && c.Name == Name && c.Town == Town && c.Postcode == Postcode && c.EORINumber == EORINumber && c.IsCreatedFromAPI == false))
                    return true;
            }
            return false;
        }
        Expression<Func<Carrier, bool>> Carirer => x => x.Name == Name &&
                x.EORINumber == EORINumber &&
                x.IsCreatedFromAPI &&
                x.CountryId == Country &&
                x.Postcode == Postcode &&
                x.AddressLine1 == AddressLine1 &&
                x.AddressLine2 == AddressLine2 &&
                x.Town == Town &&
                x.CompanyId == Company;

        public async Task<Carrier> GetCarrierForApi()
        {
            return await Database.Of<Carrier>().Where(Carirer).FirstOrDefault() ?? this;
        }
        public async Task<bool> IsEORINumberValid(string eoriNumber, Country country)
        {
            if (country.Code == Constants.GBCountryCode)
            {
                return await EORIService.IsGBEoriNumberValidate(eoriNumber);
            }

            if (eoriNumber.HasValue())
            {
                var eoriLetterPart = eoriNumber.Substring(0, 2);
                var eoriNumberPart = eoriNumber.Remove(0, 2);

                if (!eoriLetterPart.All(Char.IsLetter) && eoriNumberPart.Length > LENGTH)
                    return false;
            }
            return true;
        }
    }
}