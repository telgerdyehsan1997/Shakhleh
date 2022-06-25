using MSharp;

namespace Admin.Settings
{
    class ExchangeRateFilePage : SubPage<Admin.SettingsPage>
    {
        public ExchangeRateFilePage()
        {
            Add<Modules.ExchangeRateFileList>();

        }
    }
}