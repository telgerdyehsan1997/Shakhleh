using MSharp;

namespace Admin.Settings
{
    class AuthorisedLocationsPage : SubPage<SettingsPage>
    {
        public AuthorisedLocationsPage()
        {
            Add<Modules.AuthorisedLocationsList>();
        }
    }
}
