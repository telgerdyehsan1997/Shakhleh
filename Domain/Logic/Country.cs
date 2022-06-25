namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Security;
    using Olive.Entities;
    using System.Text.RegularExpressions;

    /// <summary> 
    /// Provides the business logic for Country class.
    /// </summary>
    partial class Country
    {
        /// <summary>
        /// Creates a new Ancillary when a new Country is created, whereby the Ancillary's country property is set to the newly created Country.
        /// </summary>

        const string UKCODE = "GB";
        private readonly string[] RateCodes = new string[] { "F", "A", "G", "UT" };

        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (e.Mode == SaveMode.Insert)
                await Database.Save(new Ancillary { Country = this });
        }

        public override string ToString(string format)
        {
            if (format == "F")
            {
                return $@"{Name} {Code}".ToUpper();
            }
            return base.ToString(format);
        }

        public override async Task Validate()
        {
            await base.Validate();

            var errors = ValidateCPCStrings().GetAwaiter().GetResult();

            if (errors.Any())
                throw new ValidationException(errors.ToLinesString());

            Name = Name.CapitaliseFirstLetters();
        }

        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);
        }

        public async Task<bool> IsUsingCPC(CPC cpc)
        {
            return ImportCPCWithPreferenceId == cpc ||
                ImportCPCWithoutPreferenceId == cpc ||
                ExportCPCWithPreferenceId == cpc ||
                ExportCPCWithoutPreferenceId == cpc;
        }

        private async Task<List<string>> ValidateCPCStrings()
        {
            var result = new List<string>();

            if (HasMFN == true)
            {
                if (!IsMFNCodeValid(MFNCode1))
                    result.Add(@"MFN code 1 must contain any combination of letters, numbers and special characters (any of !""#$%&'()*+,.:;<=>?@^_`{}|~[]-\/ ).");
                if (MFNCode2.HasValue() && !IsMFNCodeValid(MFNCode2))
                    result.Add(@"MFN code 2 must contain any combination of letters, numbers and special characters (any of !""#$%&'()*+,.:;<=>?@^_`{}|~[]-\/ ).");
                if (MFNCode3.HasValue() && !IsMFNCodeValid(MFNCode3))
                    result.Add(@"MFN code 3 must contain any combination of letters, numbers and special characters (any of !""#$%&'()*+,.:;<=>?@^_`{}|~[]-\/ ).");
                if (MFNCode4.HasValue() && !IsMFNCodeValid(MFNCode4))
                    result.Add(@"MFN code 4 must contain any combination of letters, numbers and special characters (any of !""#$%&'()*+,.:;<=>?@^_`{}|~[]-\/ ).");
                if (MFNCode5.HasValue() && !IsMFNCodeValid(MFNCode5))
                    result.Add(@"MFN code 5 must contain any combination of letters, numbers and special characters (any of !""#$%&'()*+,.:;<=>?@^_`{}|~[]-\/ ).");
            }
            if (PreferenceAvailable == true && !IsAlpha(ImportCPCWithPreferenceDeclarationType))
                result.Add("Import CPC with preference declaration type must only contain letters.");
            if (PreferenceAvailable == true && !IsNumber(ImportCPCWithPreferencePreferenceCode))
                result.Add("Import CPC with preference preference code must only contain numbers.");
            if (PreferenceAvailable == true && ImportCPCWithPreferenceRateCode.HasValue() && ImportCPCWithPreferenceRateCode.IsNoneOf(RateCodes))
                result.Add($"Import CPC with preference Rate Code must be either {RateCodes.ToString(", ")}.");
            if (!IsAlpha(ImportCPCWithoutPreferenceDeclarationType))
                result.Add("Import CPC without preference declaration type must only contain letters.");
            if (!IsNumber(ImportCPCWithoutPreferencePreferenceCode))
                result.Add("Import CPC without preference preference code must only contain numbers.");
            if (ImportCPCWithoutPreferenceRateCode.HasValue() && ImportCPCWithoutPreferenceRateCode.IsNoneOf(RateCodes))
                result.Add($"Import CPC without preference Rate Code must be either {RateCodes.ToString(", ")}.");
            if (PreferenceAvailable == true && !IsAlpha(ExportCPCWithPreferenceDeclarationType))
                result.Add("Export CPC with preference declaration type must only contain letters.");
            if (!IsAlpha(ExportCPCWithoutPreferenceDeclarationType))
                result.Add("Export CPC without preference declaration type must only contain letters.");
            if (!IsAlphaNumeric(InvoiceDeclarationDocumentType))
                result.Add("Invoice declaration document type must only contain letters and numbers.");
            if (!IsAlphaNumeric(InvoiceDeclarationDocumentTypeDocumentStatus))
                result.Add("Invoice declaration document type document status must only contain letters and numbers.");
            if (!IsAlphaNumeric(PreferenceCertificateNumberDocumentType))
                result.Add("Preference certificate document type must only contain letters and numbers.");
            if (!IsAlphaNumeric(PreferenceCertificateNumberDocumentTypeDocumentStatus))
                result.Add("Preference certification document type document status must only contain letters and numbers.");

            return result;
        }

        private bool IsMFNCodeValid(string mfnCode)
            => Regex.IsMatch(mfnCode, @"^[a-zA-Z0-9\d+!""#$%&'()*+,.:;<=>?@^_`{}|~[\]\-\\\/]+$");
        private bool IsAlpha(string value)
            => value.All(c => c.IsLetter());
        private bool IsNumber(string value)
            => value.All(c => c.IsDigit());
        private bool IsAlphaNumeric(string value)
            => value.All(c => c.IsLetterOrDigit());


        public static Task<Country> GetCountry(string countryCode)
        {
            return Database.FirstOrDefault<Country>(x => x.Code == countryCode && !x.IsDeactivated);
        }

        public static async Task<IEnumerable<Country>> GetActiveOrderedCountries(bool includeUK = true)
        {
            var result = new List<Country>();
            if (includeUK)
                result.Add(await GetCountry(UKCODE));
            result.AddRange(await Database.Of<Country>()
                .Where(x => x.Code != UKCODE && !x.IsDeactivated)
                .OrderBy(x => x.Code).GetList());
            return result;
        }

        public static Task<Country> GetUK() => GetCountry(UKCODE);


        public async static Task<Country> FindByCodeOrName(string code, string name, string searchingFor)
        {
            var country = await Database.GetList<Country>(c => c.Code == code && !c.IsDeactivated);

            if (country.IsSingle())
                return country.FirstOrDefault();
            else if (country.HasMany() && name.HasValue())
                return country.FirstOrDefault(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            else if (country.HasMany() && name.IsEmpty())
                throw new ValidationException($"{searchingFor} CountryCode has more than one country, please provide name along with the CountryCode.");
            else if (country == null)
                throw new ValidationException($"Invalid {searchingFor} Country or does not exist.");

            return country.FirstOrDefault();
        }
        public static Task<Country> GetCountryByName(string name)
        {
            var country = Database.FirstOrDefault<Country>(x => x.Name == name);
            if (country == null) { throw new ValidationException("Invalid country or does not exist."); }
            return country;
        }

    }
}
