using MSharp;

namespace Admin.Settings.Currencies
{
    class EnterPage : SubPage<Admin.Settings.CurrenciesPage>
    {
        public EnterPage()
        {
            Add<Modules.CurrencyForm>();
        }
    }
}
