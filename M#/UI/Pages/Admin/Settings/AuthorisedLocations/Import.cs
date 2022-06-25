using MSharp;

namespace Admin.Settings.AuthorisedLocations
{
    class ImportPage : SubPage<Settings.AuthorisedLocationsPage>
    {
        public ImportPage()
        {
            Add<Modules.ImportAuthorisedLocationList>();
        }
    }
}