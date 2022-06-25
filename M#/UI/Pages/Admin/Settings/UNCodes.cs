using MSharp;

namespace Admin.Settings
{
    class UNCodesPage : SubPage<Admin.SettingsPage>
    {
        public UNCodesPage()
        {
            Add<Modules.UNCodesList>();
        }
    }
}