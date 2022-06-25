using MSharp;

namespace Customer.Settings
{
    class StatusEmailNotificationsUserCustomerPage : SubPage<SettingsPage>
    {
        public StatusEmailNotificationsUserCustomerPage()
        {
            Roles(AppRole.Customer);
            Add<Modules.StatusEmailNotificationsUserCustomerShipmentList>();
        }
    }
}