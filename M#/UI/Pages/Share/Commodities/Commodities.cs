using MSharp;

namespace Share.Commodities
{
    class CommoditiesPage : RootPage
    {
        public CommoditiesPage()
        {
            BrowserTitle("Shipments > Consignments > Commodities");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.CommoditiesWeightView>();
            Add<Modules.CommodityList>();
        }
    }
}