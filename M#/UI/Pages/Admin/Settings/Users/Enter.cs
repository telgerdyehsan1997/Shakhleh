using MSharp;
using Domain;

namespace Admin.Settings.Users
{
    public class EnterPage : SubPage<UsersPage>
    {
        public EnterPage()
        {
            Add<Modules.ChannelPortsUserForm>();
        }
    }
}