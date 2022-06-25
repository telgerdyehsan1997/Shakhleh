using MSharp;
using Domain;

namespace Customer.Settings
{
    public class CustomerContactGroupsPage : SubPage<SettingsPage>
    {
        public CustomerContactGroupsPage()
        {
            Add<Modules.CustomerContactGroupList>();
        }
    }
}