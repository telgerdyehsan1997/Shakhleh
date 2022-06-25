using MSharp;

namespace Admin.Settings.Licences
{
    class StatusCodeEnterPage : SubPage<Settings.LicencesPage>
    {
        public StatusCodeEnterPage()
        {
            Add<Modules.LicenceStatusCodeForm>();
        }
    }
}