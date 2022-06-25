using MSharp;

namespace Admin.Settings.Licences
{
    class EnterPage : SubPage<Settings.LicencesPage>
    {
        public EnterPage()
        {
            Add<Modules.LicenceForm>();
        }
    }
}