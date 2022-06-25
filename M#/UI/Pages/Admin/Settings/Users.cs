using MSharp;
using Domain;

namespace Admin.Settings
{
    public class UsersPage : SubPage<SettingsPage>
    {
        public UsersPage()
        {
            Add<Modules.ChannelPortsUserList>();
        }
    }
}