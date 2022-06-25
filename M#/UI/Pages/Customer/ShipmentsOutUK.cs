using MSharp;

namespace Customer
{
    class ShipmentsOutUKPage : RootPage
    {
        public ShipmentsOutUKPage()
        {
            BrowserTitle("Shipments Out of UK");
            Roles(AppRole.Customer);
            Add<Modules.CustomerShipmentOutList>();
            OnStart(x =>
            {
                x.CSharp(@"if (Request.Query[""Id""].ToString().HasValue())
                {
                    var consigmentSearch = await Database.Of<ConsignmentSearch>().Where(x => x.ID == new Guid(Request.Query[""Id""])).FirstOrDefault();
                    if (consigmentSearch != null && ((consigmentSearch?.DateCons.HasValue ?? false) || (consigmentSearch?.DateMaxCons.HasValue ?? false) || (consigmentSearch?.ExpectedDateCons.HasValue ?? false) || (consigmentSearch?.ExpectedDateMaxCons.HasValue ?? false)))
                    {
                        DateSet(info,consigmentSearch);
                        info.Items = await GetSource(info).Select(item => new vm.CustomerShipmentOutList.ListItem(item)).ToList();
                    }
                }");
                x.CSharp(@"var url = await SearchUrlCookie.GetUrl(isNcts: false, isIntoUk: false);");
                x.If("!(Request.IsAjaxGet() && Request.QueryString.HasValue) &&  Request.IsGet() && url.HasValue() && Request.ToRawUrl() != url")
                .CSharp("return Redirect(url);");
            });
        }
    }
}