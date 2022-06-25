using MSharp;
using Domain;

namespace Customer.Settings
{
    public class CustomerContactsPage : SubPage<SettingsPage>
    {
        public CustomerContactsPage()
        {
            Add<Modules.CustomerContactList>();
        }
    }
}