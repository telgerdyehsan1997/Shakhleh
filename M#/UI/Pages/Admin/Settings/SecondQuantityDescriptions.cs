using MSharp;

namespace Admin.Settings
{
    class SecondQuantityDescriptionsPage : SubPage<Admin.SettingsPage>
    {
        public SecondQuantityDescriptionsPage()
        {
            Add<Modules.SecondQuantityDescriptionsList>();
        }
    }
}