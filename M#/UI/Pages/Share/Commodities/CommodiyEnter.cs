using MSharp;

namespace Share.Commodities
{
    class CommodiyEnterPage : SubPage<CommoditiesPage>
    {
        public CommodiyEnterPage()
        {
            BrowserTitle("Shipments > Consignments > Commodities > Enter");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.CommodityDetails>();
        }
    }
}