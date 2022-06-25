using MSharp;

namespace Admin.Settings
{
    class GlobalPage : SubPage<Admin.SettingsPage>
    {
        public GlobalPage()
        {
            Add<Modules.GlobalSettingsForm>();
            BaseController("MFABaseController");
        }
    }
}