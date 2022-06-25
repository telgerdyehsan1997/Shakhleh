using MSharp;

namespace Admin.Settings
{
    class TransitOfficesPage : SubPage<Admin.SettingsPage>
    {
        public TransitOfficesPage()
        {
            Add<Modules.TransitOfficeList>();
        }
    }
}