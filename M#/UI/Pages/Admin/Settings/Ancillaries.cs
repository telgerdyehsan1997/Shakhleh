using MSharp;

namespace Admin.Settings
{
    class AncillariesPage : SubPage<Admin.SettingsPage>
    {
        public AncillariesPage()
        {
            Add<Modules.AncillaryList>();
        }
    }
}