using MSharp;

namespace Admin.Settings
{
    class CurrenciesPage : SubPage<Admin.SettingsPage>
    {
        public CurrenciesPage()
        {
            Add<Modules.CurrencyList>();
        }
    }
}
