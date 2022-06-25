using MSharp;

namespace Admin.Settings
{
    class StatusEmailNotificationsPage : SubPage<Admin.SettingsPage>
    {
        public StatusEmailNotificationsPage()
        {
            Add<Modules.StatusEmailNotificationsShipmentList>();
        }
    }
}