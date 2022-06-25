using MSharp;

namespace Share.Commodities
{
    class CommoditiesDerermentNumberWarningPage : RootPage
    {
        public CommoditiesDerermentNumberWarningPage()
        {
            BrowserTitle("NCTS Shipments Out of UK > Consignments > Commodities > Warning");
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.CommoditiesDerermentNumberWarningView>();
        }
    }
}