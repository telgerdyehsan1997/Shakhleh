using MSharp;

namespace Admin.Settings.AuthorisedLocations
{
    class ErrorsPage : SubPage<Settings.AuthorisedLocationsPage>
    {
        public ErrorsPage()
        {
            Add<Modules.ImportErrorList>();
        }
    }
}