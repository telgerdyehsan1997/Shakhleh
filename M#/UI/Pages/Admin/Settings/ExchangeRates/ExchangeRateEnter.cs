using MSharp;

namespace Admin.Settings
{
    class ExchangeRateEnterPage : SubPage<SettingsPage>
    {
        public ExchangeRateEnterPage()
        {
            BrowserTitle("Exchange > Rate > EnterPage");
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.ExchangeRateEnter>();
        }
    }
}