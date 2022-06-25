using MSharp;

namespace Admin.Settings
{
    class GuaranteeLenghtEnterPage : SubPage<Admin.SettingsPage>
    {
        public GuaranteeLenghtEnterPage()
        {
            Roles(AppRole.Admin);
            Layout(Layouts.FrontEndModal);
            Add<Modules.GuaranteeLengthForm>();
        }
    }
}