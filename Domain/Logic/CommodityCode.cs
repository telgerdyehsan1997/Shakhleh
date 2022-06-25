namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class CommodityCode
    {
        public override async Task Validate()
        {
            await base.Validate();

            if (EUQuota.HasValue())
            {
                //if (EUQuota.Substring(0, 1) != "0")
                //    throw new ValidationException("EU Quota can start with 0 (zero)");
                if (EUQuota.Length != 6)
                    throw new ValidationException("EU Quota must be 6 numeric");
                try
                {
                    var onlyNumeric = Convert.ToInt32(EUQuota);

                }
                catch (Exception)
                {
                    throw new ValidationException("EU Quota must be 6 numeric");
                }
            }

        }
        public bool HasFullRateOfDuty => (FullRateOfDuty > 0m || (FullRateOfDuty == 0m && OtherQuota == true));
        public bool HasSpecificRate => SpecificRate.To<double>() > 0;

        public static Task<IEnumerable<CommodityCode>> GetCommodityCode()
           => Database.Of<CommodityCode>().Where(x => !x.IsDeactivated).GetList();

        public static Task<CommodityCode> FindByExportCodeAndImportCode(string exportCode, string importCode)
        {
            return Database.FirstOrDefault<CommodityCode>(c => c.ExportCode == exportCode && c.ImportCode == importCode && !c.IsDeactivated);
        }


        [Calculated]
        public IEnumerable<VATType> VatTypesToSave { get; set; }


        public async Task<VATType> GetSOrFirst()
        {
            var vatTypes = await MultipleVAT;
            return vatTypes.FirstOrDefault(x => x.Name == "S") ?? vatTypes.FirstOrDefault();
        }
    }

}