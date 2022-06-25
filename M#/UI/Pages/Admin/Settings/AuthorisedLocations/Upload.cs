using MSharp;

namespace Admin.Settings.AuthorisedLocations
{
    class UploadPage : SubPage<Settings.AuthorisedLocationsPage>
    {
        public UploadPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.ImportAuthorisedLocationForm>();
        }
    }
}