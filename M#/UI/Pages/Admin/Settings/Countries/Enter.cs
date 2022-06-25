using MSharp;

namespace Admin.Settings.Countries
{
    class EnterPage : SubPage<Settings.CountriesPage>
    {
        public EnterPage()
        {
            Add<Modules.CountryForm>();
        }
    }
}