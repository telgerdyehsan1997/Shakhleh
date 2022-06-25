using MSharp;
using Domain;

namespace Admin.Settings.Users
{
    public class ViewPage : SubPage<UsersPage>
    {
        public ViewPage()
        {
            Add<Modules.ChannelPortsUserView>();
        }
    }
}