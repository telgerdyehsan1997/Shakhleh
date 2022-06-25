namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Olive;
    using Olive.Security;
    using Olive.Entities;
    using Olive.Entities.Data;
    using System.Text.RegularExpressions;
    using Domain.AEB.DTOs;
    using System.Text;
    using APIHandler;
    using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

    partial class Company
    {
        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const int LENGTH = 20;
        const int DataSize = 50;

        private readonly static Random Random = new Random();
        IEORIService EORIService = Context.Current.GetService<IEORIService>();
        public override async Task Validate()
        {
            await base.Validate();

            await ValidateFields();
        }

        async Task ValidateFields()
        {
            Town = Town.CapitaliseFirstLetters();

            var result = new List<string>();

            if (TSP.HasValue() && !TSP.All(x => x.IsLetterOrDigit()))
                result.Add("TSP may only contain alphanumeric characters.");

            if (CustomerAccountNumber.HasValue() && !Regex.Match(CustomerAccountNumber, "[A-Z]\\d{4}").Success)
                result.Add("Customer account number must be in the format of \"A1234\".");

            if (Country.Code == "GB" && EORINumber.IsEmpty())
                result.Add("The EORI number field is required.");

            if (Country.Code == "GB" && (!EORINumber?.StartsWith("GB") ?? false))
                result.Add("The UK EORI number must start with GB.");

            if (BranchIdentifier.HasValue() && !Regex.Match(BranchIdentifier, "\\b(AG|BR)\\d{3}\\b").Success)
                result.Add("Branch Identifier is in the wrong format.");

            if (IsDefaultDeclarant && IsDeactivated)
                result.Add("You cannot set an archived Company as Default declarant.");

            var isValidEORI = await IsEORINumberValid(EORINumber, Country);
            var isValidTIN = IsTINNumberValid(TIN, Country);

            if (EORINumber.HasValue() && !isValidEORI)
                result.Add("The EORI number is invalid.");

            if (GuaranteeNumber.HasValue() && GuaranteeType.IsEmpty())
            {
                result.Add("Guarantee Type must have value if Transit Guarantee has a value.");
            }

            if (GuaranteeNumber.HasValue() && GuaranteeType.HasValue())
            {
                if (TIN.IsEmpty())
                    result.Add("TIN must have value if Transit Guarantee and Guarantee type have a value.");
                if (PIN.IsEmpty())
                    result.Add("PIN must have value if Transit Guarantee and Guarantee type have a value.");
            }
            if (TIN.HasValue() && !isValidTIN)
                result.Add("The TIN is invalid.");

            if (await IsCompanyUnique())
                result.Add("There is an existing Company with the same Deferment number, EORI number, Name, Postcode and Town/City in the database already.");

            if (CFSPTypeId.IsAnyOf(CFSPType.Own))
            {
                if (AuthorisationNumber.IsEmpty())
                    result.Add("Either Channelports CFSP or authorisation number are mandatory");
                else if (AuthorisationNumber.HasValue())
                {
                    var isValidAuthorisationNumber = await IsEORINumberValid(AuthorisationNumber, Country);
                    if (!isValidAuthorisationNumber)
                        result.Add("The Authorisation Number number is invalid.");
                }
            }
            if (AuthorisedCFSPCPCNumberId == CFSPCPCNumber._0612071 && AuthorisedWarehouseNumber.HasValue() && !IsWarehouseNumberIsValid(AuthorisedWarehouseNumber))
                result.Add("The Warehouse number is in invalid format, it should be 10 characters long (first and last 2 characters are alphabets and 7 character are numbers e.g. (A1234567AA))");

            if (result.Any())
                throw new ValidationException(result.ToLinesString());

        }

        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (e.Mode == SaveMode.Insert)
            {
                var addedBy = Context.Current.User().ExtractUser<User>();

                if (addedBy is CompanyUser)
                {
                    await (addedBy as CompanyUser).Company.AddCompanyToAssociatedCompanies(this);
                }
            }
        }

        protected override async Task OnValidating(EventArgs e)
        {
            await Validation();

            await base.OnValidating(e);
        }

        private async Task Validation()
        {
            if (TypeId == CompanyType.Other)
                RefrenceSuffix = "000";

            else if (RefrenceSuffix.IsEmpty() || RefrenceSuffix == "---")
            {
                var isduplicate = true;
                while (isduplicate)
                {
                    RefrenceSuffix = CreateSuffix();
                    if ((await FindByRefrenceSuffix(RefrenceSuffix)) == null)
                        isduplicate = false;
                }
            }

            if (this.ID != Constants.ChannelPortsID && EORINumber?.Equals(Constants.ChannelPortEORI) == true && !this.IsCreatedFromAPI)
                throw new ValidationException("You can't use this EORI.");

            if (DefermentNumber.HasValue() && DefermentNumber.StartsWith("2"))
            {
                var hasValue = await Database.Of<Company>().Where(x => x.DefermentNumber == DefermentNumber && x.ID != ID).Any();
                if (hasValue)
                    throw new ValidationException("Deferment number is already taken.please try with new one.");
            }

        }
        public string GetUkDisplayText()
        {
            var result = new StringBuilder();
            result.Append($"{Name} - {Town}");

            if (EORINumber.HasValue())
                result.Append($" - EORI {EORINumber}");

            if (DefermentNumber.HasValue())
            {
                if (DefermentNumber.StartsWith("2"))
                    result.Append(" - Deferment by deposit");
                else
                    result.Append($" - Deferment  {DefermentNumber}");
            }

            return result.ToString();
        }
        public string GetDisplayText()
        {
            var result = new StringBuilder();
            result.Append($"{Name} - {Town} - {Postcode}");

            if (EORINumber.HasValue())
                result.Append($" - {EORINumber}");

            if (DefermentNumber.HasValue())
                result.Append($" - {DefermentNumber}");

            return result.ToString();
        }
        public string GetPatnerText()
        {
            var result = new StringBuilder();
            result.Append($"{Name} - {Town} - {Postcode} - {Country.Name}");

            if (EORINumber.HasValue())
                result.Append($" - {EORINumber}");

            return result.ToString();
        }
        public async Task<bool> HaveDeposit()
        {
            return await Database.Of<Deposit>()
                   .Any(s => s.CompanyId == this && s.TransactionTypeId == TransactionType.Deposit);
        }


        public async Task GenerateBespokeRates()
        {
            foreach (var ancillary in await Database.Of<Ancillary>().Where(a => a.CompanyId == null).GetList())
            {
                await Database.Save(new Ancillary
                {
                    Company = this,
                    Country = ancillary.Country,
                    FreightChargePerTonne = ancillary.FreightChargePerTonne,
                    FullLoadFreightCharge = ancillary.FullLoadFreightCharge,
                    InsuranceCharge = ancillary.InsuranceCharge,
                    ValueForVAT = ancillary.ValueForVAT
                });
            }
        }

        public async Task<IEnumerable<Company>> GetAssociatedCompanies(string lookupText = "")
        {
            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                return await AssociatedCompanies
                    .Where(x => !x.AssociatedToCompany.IsDeactivated
                               && !x.AssociatedToCompany.IsCreatedFromAPI)
                    .GetList()
                    .Select(x => x.AssociatedToCompany)
                    .Concat(this)
                    .ExceptNull()
                    .Distinct()
                    .OrderBy(x => x.Name);
            }
            else
            {
                if (lookupText.HasValue())
                {
                    if (lookupText.Contains("-"))
                        lookupText = lookupText.Split("-")[0];

                    return await Database.Of<Company>()
                        .Where(c => !c.IsDeactivated
                                && c.IsCreatedFromAPI == false
                                && c.Name.Contains(lookupText))
                        .OrderBy(t => t.Name)
                        .GetList()
                        .Take(DataSize);
                }

                return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated && c.IsCreatedFromAPI == false)
                    .OrderBy(t => t.Name)
                    .GetList();
            }

        }

        public async Task<IEnumerable<Company>> GetDeclarentAssociatedCompanies(string lookupText = "")
        {

            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                var currantCumpany = this.EORINumber.StartsWith("GB") ? this : null;
                return await AssociatedCompanies
                    .Where(x => !x.AssociatedToCompany.IsDeactivated
                            && !x.AssociatedToCompany.IsCreatedFromAPI
                            && x.AssociatedToCompany.EORINumber.HasValue()
                            && x.AssociatedToCompany.EORINumber.StartsWith("GB"))
                    .GetList()
                    .Select(x => x.AssociatedToCompany)
                    .Concat(currantCumpany)
                    .ExceptNull()
                    .Distinct()
                    .Where(x => x.EORINumber.HasValue())
                    .OrderBy(x => x.Name);
            }
            else
            {
                if (lookupText.HasValue())
                {
                    if (lookupText.Contains("-"))
                        lookupText = lookupText.Split("-")[0].TrimEnd();

                    return await Database.Of<Company>()
                        .Where(c => !c.IsDeactivated
                            && c.IsCreatedFromAPI == false
                            && c.EORINumber.HasValue()
                            && c.EORINumber.StartsWith("GB")
                            && c.Name.Contains(lookupText))
                        .OrderBy(t => t.Name)
                        .GetList()
                        .Take(DataSize);
                }

                return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated
                            && c.IsCreatedFromAPI == false
                            && c.EORINumber.HasValue()
                            && c.EORINumber.StartsWith("GB"))
                    .OrderBy(t => t.Name)
                    .GetList();
            }
        }

        public async Task<IEnumerable<Company>> GetPartnerAssociatedCompanies(string lookupText = "")
        {
            var ukCountry = await Country.GetUK();
            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                var currantCumpany = this.CountryId != ukCountry ? this : null;
                return await AssociatedCompanies
                    .Where(x => !x.AssociatedToCompany.IsDeactivated
                            && !x.AssociatedToCompany.IsCreatedFromAPI
                            && x.AssociatedToCompany.CountryId != ukCountry)

                    .GetList()
                    .Select(x => x.AssociatedToCompany)
                    .Concat(currantCumpany)
                    .ExceptNull()
                    .Distinct()
                    .OrderBy(x => x.Name);
            }
            else
            {
                if (lookupText.HasValue())
                {
                    if (lookupText.Contains("-"))
                        lookupText = lookupText.Split("-")[0].TrimEnd();

                    return await Database.Of<Company>()
                        .Where(c => !c.IsDeactivated
                            && c.IsCreatedFromAPI == false
                            && c.CountryId != ukCountry
                            && c.Name.Contains(lookupText))

                        .OrderBy(t => t.Name)
                        .GetList()
                        .Take(DataSize);
                }

                return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated
                        && c.IsCreatedFromAPI == false
                        && c.CountryId != ukCountry)
                    .OrderBy(t => t.Name)
                    .GetList();
            }

        }

        public async Task<IEnumerable<Company>> GetTraderAssociatedCompanies(string lookupText = "")
        {
            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                var currantCumpany = this.EORINumber.StartsWith("GB") ? this : null;
                return await AssociatedCompanies
                    .Where(x => !x.AssociatedToCompany.IsDeactivated
                            && !x.AssociatedToCompany.IsCreatedFromAPI
                            && x.AssociatedToCompany.EORINumber != Constants.ChannelPortEORI
                            && x.AssociatedToCompany.EORINumber.StartsWith("GB"))

                    .GetList()
                    .Select(x => x.AssociatedToCompany)
                    .Concat(currantCumpany)
                    .ExceptNull()
                    .Distinct()
                    .OrderBy(x => x.Name)
                    .Where(x => x.EORINumber.HasValue());
            }
            else
            {
                if (lookupText.HasValue())
                {
                    if (lookupText.Contains("-"))
                        lookupText = lookupText.Split("-")[0].TrimEnd();

                    return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated
                                && c.IsCreatedFromAPI == false
                                && c.EORINumber != Constants.ChannelPortEORI
                                && c.EORINumber.StartsWith("GB")
                                && c.Name.Contains(lookupText))

                    .OrderBy(t => t.Name)
                    .Where(x => x.EORINumber.HasValue())
                    .GetList()
                    .Take(DataSize);
                }

                return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated
                        && c.IsCreatedFromAPI == false
                        && c.EORINumber.HasValue()
                        && c.EORINumber != Constants.ChannelPortEORI
                        && c.EORINumber.StartsWith("GB"))

                    .OrderBy(t => t.Name)
                    .GetList();
            }
        }

        internal async Task AddCompaniesToAssociatedCompanies(Consignment consignment)
        {
            var companies = new List<Company> { consignment.UKTrader, consignment.Partner, consignment.Declarant }.ToList().Distinct();

            foreach (var company in companies.ToList())
            {
                if (company == this)
                    continue;

                await AddCompanyToAssociatedCompanies(company);
            }
        }


        public async Task AddCompanyToAssociatedCompanies(Company company)
        {
            if ((await AssociatedCompanies.GetList().ToList()).None(a => a.AssociatedToCompanyId == company))
                await Database.Save(new CompanyAssociationLink { Company = this, AssociatedToCompany = company });
        }

        internal PartyDTO ToPartyDTO()
        {
            return new PartyDTO
            {
                PartyType = "???",
                CustomsProcess = "???",
                CompanyNumber = CustomerAccountNumber,
                Name = Name,
                Street = AddressLine1,
                Street2 = AddressLine2,
                City = Town,
                PostCode = Postcode,
                Country = Country.Code,
                CustomsIds = new List<CustomsIdentifcationDTO> { new CustomsIdentifcationDTO { IdentificationType = "???", Identification = "???" } },
            };
        }


        public async Task<IEnumerable<Person>> GetAvailableContacts()
        {

            var result = new List<Person>();
            result.AddRange(await Contacts.Where(x => !x.IsDeactivated).GetList().Select(u => u as Person).ToList());
            result.AddRange(await CompanyUsers.Where(x => (x.AccountsDepartment == false || (x.AccountsDepartment == true && x.IsAdmin == true)) && !x.IsDeactivated).GetList().Select(u => u as Person).ToList());

            return result.OrderBy(u => u.Name);
        }

        public async Task<bool> IsEORINumberValid(string eoriNumber, Country country)
        {

            if (eoriNumber.IsEmpty() || eoriNumber.Length < 2)
                return false;


            if (eoriNumber.HasValue())
            {
                var eoriLetterPart = eoriNumber.Substring(0, 2);
                var eoriNumberPart = eoriNumber.Remove(0, 2);

                if (eoriLetterPart.Any(x => !Char.IsLetter(x)))
                    return false;

                if (eoriLetterPart == Constants.GBCountryCode)
                {
                    if (eoriNumber.Length != 14)
                        return false;

                    return await EORIService.IsGBEoriNumberValidate(eoriNumber);
                }



                if (!eoriLetterPart.All(Char.IsLetter) && eoriNumberPart.Length > LENGTH)
                    return false;
            }
            return true;
        }

        public bool IsTINNumberValid(string eoriNumber, Country country)
        {
            if (eoriNumber.IsEmpty()) return false;

            var result = true;

            var eoriLetterPart = eoriNumber.Substring(0, 2);
            var eoriNumberPart = eoriNumber.Remove(0, 2);

            if (!eoriLetterPart.All(Char.IsLetter))
                result = false;

            if (country.Code == "GB" && eoriLetterPart != "GB")
                result = false;

            if (country.Code == "GB" && eoriNumberPart.Length != 12)
                result = false;

            if (country.Code != "GB" && eoriNumberPart.Length > LENGTH)
                result = false;

            if (!result && country.Code != "GB")
                result = EORIService.IsEoriNumberValidate(eoriNumber);

            return result;
        }

        public bool IsWarehouseNumberIsValid(string warehouseNumber)
        {
            if (warehouseNumber.IsEmpty())
                return false;

            if (warehouseNumber.Length != 10)
                return false;

            var firstLetterPart = warehouseNumber.Substring(0, 1);
            var secondLetterPart = warehouseNumber.Substring(8, 2);

            if (!firstLetterPart.All(Char.IsLetter) && !secondLetterPart.All(Char.IsLetter))
                return false;

            var numberPart = warehouseNumber.Substring(1, 7);

            if (!numberPart.All(Char.IsDigit))
                return false;

            return true;
        }

        public Task<IEnumerable<Shipment>> InShipments => Shipments.Where(s => s.TypeId == ShipmentType.IntoUk).GetList();

        public Task<IEnumerable<Shipment>> OutShipments => Shipments.Where(s => s.TypeId == ShipmentType.OutOfUk).GetList();

        public Task<bool> IsUsingCPC(CPC cpc)
        {
            return SpecialCPCs.Where(x => x.CPCId == cpc).Any();
        }

        private async Task<bool> IsCompanyUnique()
        {
            if (EORINumber.HasValue() && DefermentNumber.HasValue() && !IsCreatedFromAPI)
            {
                if (await Database.Any<Company>(c => c != this && c.Name == Name && c.Town == Town && c.Postcode == Postcode && c.EORINumber == EORINumber && c.DefermentNumber == DefermentNumber && c.IsCreatedFromAPI == false))
                {
                    return true;
                }
            }
            return false;
        }

        public static Task<Company> FindByDefermentNumberAndEORINumberAndNameAndPostcodeAndTown(string defermentNumber, string eORINumber, string name, string postcode, string town)
        {
            return Database.FirstOrDefault<Company>(c => c.DefermentNumber == defermentNumber && c.EORINumber == eORINumber && c.Name == name && c.Postcode == postcode && c.Town == town);
        }

        public static async Task<IEnumerable<Company>> UKCompanies()
        {
            var uk = await Country.GetUK();
            return await Database.Of<Company>()
                .Where(x => x.CountryId == uk && !x.IsCreatedFromAPI)
                .GetList();
        }

        public static async Task<IEnumerable<Company>> NonUKCompanies()
        {
            var uk = await Country.GetUK();
            return await Database.Of<Company>()
                .Where(x => x.CountryId != uk && !x.IsCreatedFromAPI)
                .GetList();
        }

        public async Task<bool> CanDoEAD() => await TransactionTypes.Any();

        public string CreateSuffix()
        {
            return new string(Enumerable.Repeat(CHARS, 3)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private Task<Company> FindByRefrenceSuffix(string suffix)
        {
            return Database.FirstOrDefault<Company>(t => t.RefrenceSuffix == suffix);
        }

        public static Task<Company> ChannelPort => Database.FirstOrDefault<Company>(c => c.ID == Constants.ChannelPortsID);

        public bool IsMonthlyInvoice => InvoiceFrequencyId != InvoiceFrequencyType.Yearly;

        public async Task<(string rate, int value)> GetVat()
        {
            var vat = (await Database.Of<VATRate>().Where(t => !t.IsDeactivated).GetList()).ToList().LastOrDefault();
            return Country.Code == "GB" ? ("S", vat.RateS) : ("Z", vat.RateZ);
        }


        public async Task<Company> GetCompanyForApi()
        {
            if (DefermentNumber.HasValue() && DefermentNumber.StartsWith("2"))
                return await Database.Of<Company>().Where(x => x.DefermentNumber == DefermentNumber).FirstOrDefault();

            var filter = await Database.Of<Company>()
                .Where(x =>
                x.Name == Name &&
                x.EORINumber == EORINumber &&
                x.PaymentTypeId == PaymentType &&
                x.DefermentNumber == DefermentNumber &&
                x.IsCreatedFromAPI
            )
                .Where(x =>
                x.CountryId == Country &&
                x.Postcode == Postcode &&
                x.AddressLine1 == AddressLine1 &&
                x.AddressLine2 == AddressLine2 &&
                x.Town == Town
            )
                .FirstOrDefault();

            return filter ?? this;
        }

        public async Task<decimal> GetRemainingBalance(Deposit deposit)
        {
            var deposits = await Deposits.Where(d => d.DateAdded <= deposit.DateAdded && d.TransactionTypeId == TransactionType.Deposit && !d.IsDeactivated).Sum(d => d.Value);
            var withdrawal = await Deposits.Where(d => d.DateAdded <= deposit.DateAdded && d.TransactionTypeId == TransactionType.Withdrawal && !d.IsDeactivated).Sum(d => d.Value);

            return (deposits ?? 0) - (withdrawal ?? 0);
        }
        public async Task<decimal?> GetTotalRemainingBalance()
        {
            return (await Deposits.Where(x => !x.IsDeactivated)
                .WithMax(x => x.DateAdded))?.RemainingBalance ?? 0;
        }

        public async Task<decimal?> GetPendingTransactionValue()
        {
            return (await Deposits.Where(x => !x.IsDeactivated && x.TransactionTypeId == TransactionType.Pending).Sum(x => x.Value)) ?? 0;
        }

        public async Task<decimal> GetRemainingBalanceAfterPending()
        {
            return (await GetTotalRemainingBalance() ?? 0) - (await GetPendingTransactionValue() ?? 0);
        }

        public static async Task<List<Guid?>> ShipmentList(ConsignmentSearch parameters)
        {
            var result = Database.Of<Consignment>();

            if (parameters.ConsignmentNumber.HasValue())
                result = result.Where(x => x.ConsignmentNumber == parameters.ConsignmentNumber);
            if (parameters.UKTraderId.HasValue)
                result = result.Where(x => x.UKTraderId == parameters.UKTraderId);
            if (parameters.DeclarantId.HasValue)
                result = result.Where(x => x.DeclarantId == parameters.DeclarantId);
            if (parameters.TotalGrossWeightMin > 0)
                result = result.Where(x => x.TotalGrossWeight >= parameters.TotalGrossWeightMin);
            if (parameters.TotalGrossWeightMax > 0)
                result = result.Where(x => x.TotalGrossWeight <= parameters.TotalGrossWeightMax);
            if (parameters.InvoiceNumber.HasValue())
                result = result.Where(x => x.InvoiceNumber == parameters.InvoiceNumber);
            if (parameters.TotalValueMin > 0)
                result = result.Where(x => x.TotalValue >= parameters.TotalValueMin);
            if (parameters.TotalValueMax > 0)
                result = result.Where(x => x.TotalValue <= parameters.TotalValueMax);
            if (parameters.UCR.HasValue())
                result = result.Where(x => x.UCR == parameters.UCR);
            if (parameters.PartnerId.HasValue)
                result = result.Where(x => x.PartnerId == parameters.PartnerId);
            if (parameters.TotalPackagesMin > 0)
                result = result.Where(x => x.TotalPackages >= parameters.TotalPackagesMin);
            if (parameters.TotalPackagesMax > 0)
                result = result.Where(x => x.TotalPackages <= parameters.TotalPackagesMax);
            if (parameters.TotalNetWeightMin > 0)
                result = result.Where(x => x.TotalNetWeight >= parameters.TotalNetWeightMin);
            if (parameters.TotalNetWeightMax > 0)
                result = result.Where(x => x.TotalNetWeight <= parameters.TotalNetWeightMax);
            if (parameters.InvoiceCurrencyId.HasValue)
                result = result.Where(x => x.InvoiceCurrencyId == parameters.InvoiceCurrencyId);
            if (parameters.ProgressId.HasValue)
                result = result.Where(x => x.ProgressId == parameters.ProgressId);
            return await result.GetList().Select(x => x.ShipmentId).ToList();
        }

        public static Task<IEnumerable<Company>> GetCompanyList()
        {
            return Database.Of<Company>()
                .Where(c => c.IsCreatedFromAPI == false)
                .OrderBy(x => x.Name)
                .GetList();
        }

        //for search
        public async Task<IEnumerable<Company>> GetSearchTraderAssociatedCompanies()
        {
            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                var shipments = await Database.Of<Shipment>().Where(x => x.CompanyId == this).GetList().Select(x => x.ID).ToList();
                var ukTradersId = await Database.Of<Consignment>()
                    .Where(x => x.ShipmentId.IsAnyOf(shipments))
                    .GetList()
                    .Select(x => x.UKTraderId)
                    .ExceptNull()
                    .Distinct()
                    .ToList();

                return await Database.Of<Company>()
                    .Where(x => x.ID.IsAnyOf(ukTradersId) && x.EORINumber.StartsWith("GB"))
                    .GetList();
            }
            else
                return await Database.Of<Company>().Where(c => !c.IsDeactivated && c.IsCreatedFromAPI == false
                                                        && c.EORINumber != Constants.ChannelPortEORI && c.EORINumber.HasValue()
                                                        && c.EORINumber.StartsWith("GB")).OrderBy(t => t.Name).GetList();
        }
        public async Task<IEnumerable<Company>> GetSearchPatnerAssociatedCompanies()
        {
            var ukCountry = await Country.GetUK();
            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                var shipments = await Database.Of<Shipment>().Where(x => x.CompanyId == this).GetList().Select(x => x.ID).ToList();
                var partnerId = await Database.Of<Consignment>()
                    .Where(x => x.ShipmentId.IsAnyOf(shipments))
                    .GetList().Select(x => x.PartnerId)
                    .ExceptNull()
                    .Distinct()
                    .ToList();

                return await Database.Of<Company>()
                    .Where(x => x.ID.IsAnyOf(partnerId))
                    .GetList();
            }
            else
                return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated && c.IsCreatedFromAPI == false && c.CountryId != ukCountry)
                    .OrderBy(t => t.Name)
                    .GetList();
        }
        public async Task<IEnumerable<Company>> GetSearchDeclerentAssociatedCompanies()
        {
            var ukCountry = await Country.GetUK();
            if (Context.Current.User().ExtractUser<User>() is CompanyUser)
            {
                var shipments = await Database.Of<Shipment>().Where(x => x.CompanyId == this).GetList().Select(x => x.ID).ToList();
                var declarantId = await Database.Of<Consignment>()
                    .Where(x => x.ShipmentId.IsAnyOf(shipments))
                    .GetList()
                    .Select(x => x.DeclarantId)
                    .ExceptNull()
                    .Distinct()
                    .ToList();

                return await Database.Of<Company>()
                    .Where(x => x.ID.IsAnyOf(declarantId) && x.EORINumber.StartsWith("GB"))
                    .GetList();
            }
            else
                return await Database.Of<Company>()
                    .Where(c => !c.IsDeactivated && c.IsCreatedFromAPI == false && c.EORINumber.HasValue() && c.EORINumber.StartsWith("GB"))
                    .OrderBy(t => t.Name).GetList();
        }

        public async Task<bool> IsEORINumberValid(string eoriNumber)
        {
            if (eoriNumber.HasValue())
            {
                var isValid = await Database.Of<Company>().Any(x => x.EORINumber == eoriNumber.Trim());
                if (!isValid)
                    return false;

                return await EORIService.IsGBEoriNumberValidate(eoriNumber);
            }
            return false;
        }

        public async Task<bool> IsEORINumberValid(Shipment shipment, string eoriNumber)
        {
            if (eoriNumber.HasValue())
            {
                return shipment.Company.EORINumber == eoriNumber.Trim() ||
                    await shipment.Consignments.GetList().Any(x => x.UKTrader.EORINumber == eoriNumber.Trim())
                    || await shipment.Consignments.GetList().Any(x => x.Declarant.EORINumber == eoriNumber.Trim());
            }
            return false;
        }
    }
}