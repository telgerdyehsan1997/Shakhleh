using MSharp;

namespace Admin.Settings.AuthorisedLocations
{
    class EnterPage : SubPage<AuthorisedLocationsPage>
    {
        public EnterPage()
        {
            Add<Modules.AuthorisedLocationsForm>();
        }
    }
}
