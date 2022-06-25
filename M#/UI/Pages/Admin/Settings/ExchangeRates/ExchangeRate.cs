using MSharp;

namespace Admin.Settings
{
    class ExchangeRatePage : SubPage<Admin.SettingsPage>
    {
        public ExchangeRatePage()
        {
            Add<Modules.ExchangeRateList>();
        }
    }
}