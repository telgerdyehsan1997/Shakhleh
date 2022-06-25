using MSharp;

namespace Admin.Settings
{
    class LicencesPage : SubPage<Admin.SettingsPage>
    {
        public LicencesPage()
        {
            Add<Modules.LicencesList>();
            Add<Modules.LicenceStatusCodeList>();
        }
    }
}