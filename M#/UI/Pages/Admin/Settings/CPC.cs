using MSharp;

namespace Admin.Settings
{
    class CPCPage : SubPage<Admin.SettingsPage>
    {
        public CPCPage()
        {
            Add<Modules.CPCList>();
        }
    }
}