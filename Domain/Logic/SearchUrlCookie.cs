namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class SearchUrlCookie
    {
        public static async Task<string> GetUrl(bool isNcts = false, bool isIntoUk = false)
        {
            return (await Database.Of<SearchUrlCookie>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault())?.Url;

        }

        public static async Task ClearCookieHistory(bool isNcts = false, bool isIntoUk = false)
        {
            var search = await Database.Of<SearchUrlCookie>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault();

            if (search != null)
            {
                var consigmentSearch = await Database.Of<ConsignmentSearch>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault();
                if (consigmentSearch != null)
                {
                    var newUrl = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(Context.Current.Request().GetReturnUrl(), "ID", consigmentSearch.ID.ToString());

                    if (Context.Current.Request().GetReturnUrl().IsEmpty())
                    {
                        newUrl = search.Url.Split("?")[0] + newUrl;
                    }
                    await Database.Update(search, x => x.Url = newUrl);
                }
                else
                {
                    await Database.DeleteAll<SearchUrlCookie>(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk);
                }
            }
        }
        public static async Task CreateCookieHistory(DateTime? date = null, DateTime? dateMax = null, DateTime? exp = null, DateTime? expMax = null, bool isNcts = false, bool isIntoUk = false)
        {
            var context = Context.Current.Request();
            if (context.IsGet() && context.QueryString.HasValue)
            {
                var previousUrl = Database.Of<SearchUrlCookie>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk);
                if (await previousUrl.Count() > 0)
                {
                    await Database.DeleteAll<SearchUrlCookie>(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk);
                }
                var searchUrlCookie = new SearchUrlCookie
                {
                    User = Context.Current.User().ExtractUser<User>(),
                    Url = context.ToRawUrl().Split("_modal")[0],
                    IsNcts = isNcts,
                    IsIntoUk = isIntoUk
                };
                await Database.Save(searchUrlCookie);

                if (exp.HasValue || expMax.HasValue || date != LocalTime.Now.Add(-3.Days()).Date || dateMax != LocalTime.Now.Add(3.Days()).Date)
                {
                    var consigmentSearch = await Database.Of<ConsignmentSearch>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault();
                    if (consigmentSearch != null)
                    {
                        await Database.Update(consigmentSearch, x =>
                        {
                            x.DateCons = date ?? consigmentSearch.DateCons;
                            x.DateMaxCons = dateMax ?? consigmentSearch.DateMaxCons;
                            x.ExpectedDateCons = exp ?? consigmentSearch.ExpectedDateCons;
                            x.ExpectedDateMaxCons = expMax ?? consigmentSearch.ExpectedDateMaxCons;
                        });
                    }
                }
            }
        }
        public static async Task<bool> IsActiveShipmentSearch(bool isNcts = false, bool isIntoUk = false)
        {
            var search = await Database.Of<SearchUrlCookie>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault();
            if (search != null)
                if (search.Url.Contains("IsDeactivated"))
                    return true;

            return false;
        }
        public static async Task<bool> IsActiveConsignmentSearch(bool isNcts = false, bool isIntoUk = false)
        {
            var consigmentSearch = await Database.Of<ConsignmentSearch>().Where(x => x.UserId == Context.Current.User().ExtractUser<User>() && x.IsNcts == isNcts && x.IsIntoUk == isIntoUk).FirstOrDefault();

            if (consigmentSearch != null)
            {
                return true;
            }
            return false;
        }
    }
}

