using MSharp;

namespace Admin.Settings
{
    class GuaranteeLengthPage : SubPage<Admin.SettingsPage>
    {
        public GuaranteeLengthPage()
        {
            Roles(AppRole.Admin);
            Add<Modules.AuthorisedLocationGuaranteeLengthList>();
        }
    }
}