using MSharp;

namespace Admin.Settings
{
    class CommodityCodesPage : SubPage<Admin.SettingsPage>
    {
        public CommodityCodesPage()
        {
            Add<Modules.CommodityCodeList>();
        }
    }
}