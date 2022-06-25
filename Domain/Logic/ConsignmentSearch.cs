namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class ConsignmentSearch
    {
        public static async Task ClearCookieHistory(bool isNcts = false, bool isIntoUk = false)
        {
            var consignmentSearch = Database.Of<ConsignmentSearch>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk);
            if (await consignmentSearch.Count() > 0)
            {
                await Database.DeleteAll<ConsignmentSearch>(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk);
                var search = await Database.Of<SearchUrlCookie>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault();

                var context = Context.Current.Request();

                var url = string.Format("{0}://{1}", context.Scheme, context.Host);
                var removeId = new Uri(url + search.Url).RemoveQueryString("ID").ToString().TrimStart(url);
                await Database.Update(search, x => x.Url = removeId);

            }
        }
        public static string CreateCookieHistory(string id)
        {
            var context = Context.Current.Request();
            if (context.GetReturnUrl().Contains("?"))
                return Extensions.ReplaceQueryStringParam(context.GetReturnUrl(), "ID", id);
            else
                return Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(context.GetReturnUrl(), "ID", id);
        }
        bool HasConsigmentFiled(ConsignmentSearch consignmentSearch)
        {
            return consignmentSearch.ConsignmentNumber.HasValue() || consignmentSearch.UKTraderId.HasValue ||
                consignmentSearch.DeclarantId.HasValue || consignmentSearch.InvoiceNumber.HasValue() ||
                consignmentSearch.CommodityCode.HasValue() || consignmentSearch.UCR.HasValue() ||
                consignmentSearch.PartnerId.HasValue || consignmentSearch.InvoiceCurrencyId.HasValue ||
                consignmentSearch.ProgressId.HasValue || consignmentSearch.TotalGrossWeightMin.HasValue ||
                consignmentSearch.TotalGrossWeightMax.HasValue || consignmentSearch.TotalValueMin.HasValue ||
                consignmentSearch.TotalValueMax.HasValue || consignmentSearch.TotalPackagesMin.HasValue ||
                consignmentSearch.TotalPackagesMax.HasValue || consignmentSearch.TotalNetWeightMin.HasValue ||
                consignmentSearch.TotalNetWeightMax.HasValue;
        }
        public bool CheckConsigmentFiled(ConsignmentSearch consignmentSearch)
        {
            if (HasConsigmentFiled(consignmentSearch))
                return false;
            return true;
        }
        bool HasNctsConsigmentFiled(ConsignmentSearch consignmentSearch)
        {
            return consignmentSearch.LRN.HasValue() || consignmentSearch.EADMRN.HasValue() ||
                consignmentSearch.UKTraderId.HasValue || consignmentSearch.GuarantorId.HasValue ||
                consignmentSearch.CommodityCode.HasValue() || consignmentSearch.PartnerId.HasValue ||
                consignmentSearch.TotalGrossWeightMin.HasValue || consignmentSearch.TotalGrossWeightMax.HasValue ||
                consignmentSearch.TotalValueMin.HasValue || consignmentSearch.TotalValueMax.HasValue ||
                consignmentSearch.TotalPackagesMin.HasValue || consignmentSearch.TotalPackagesMax.HasValue || consignmentSearch.TotalNetWeightMin.HasValue || consignmentSearch.TotalNetWeightMax.HasValue;
        }
        public bool CheckNctsConsigmentFiled(ConsignmentSearch consignmentSearch)
        {
            if (HasNctsConsigmentFiled(consignmentSearch))
                return false;
            return true;
        }
    }

}