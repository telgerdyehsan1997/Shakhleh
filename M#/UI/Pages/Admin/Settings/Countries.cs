using MSharp;

namespace Admin.Settings
{
    class CountriesPage : SubPage<Admin.SettingsPage>
    {
        public CountriesPage()
        {
            Add<Modules.CountryList>();
        }
    }
}