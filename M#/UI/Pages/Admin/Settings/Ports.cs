using MSharp;

namespace Admin.Settings
{
    class PortsPage : SubPage<Admin.SettingsPage>
    {
        public PortsPage()
        {
            Add<Modules.PortsList>();
        }
    }
}