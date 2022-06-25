using MSharp;

namespace Share.Commodities
{
    class CommoditiesViewPage : RootPage
    {
        public CommoditiesViewPage()
        {
            BrowserTitle("Shipments > View > Commodities");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.CommoditiesWeightView>();
            Add<Modules.CommodityListView>();
        }
    }
}