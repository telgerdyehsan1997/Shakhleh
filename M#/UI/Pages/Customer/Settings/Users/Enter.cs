using MSharp;
using Domain;

namespace Customer.Settings.Users
{
    public class EnterPage : SubPage<UsersPage>
    {
        public EnterPage()
        {
            Add<Modules.CustomerUserForm>();
        }
    }
}