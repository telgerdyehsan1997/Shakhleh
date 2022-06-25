using MSharp;
using Domain;

namespace Customer.Settings
{
    public class UsersPage : SubPage<SettingsPage>
    {
        public UsersPage()
        {
            Add<Modules.CustomerUserList>();
        }
    }
}