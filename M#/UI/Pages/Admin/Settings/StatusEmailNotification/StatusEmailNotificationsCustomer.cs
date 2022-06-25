using MSharp;

namespace Admin.Settings
{
    class StatusEmailNotificationsCustomerPage : SubPage<Admin.SettingsPage>
    {
        public StatusEmailNotificationsCustomerPage()
        {
            Add<Modules.StatusEmailNotificationsCustomerShipmentList>();
        }
    }
}