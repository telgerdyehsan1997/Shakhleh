using MSharp;
using Domain;

namespace ShopUser.Settings.Users
{
    public class EnterPage : SubPage<UsersPage>
    {
        public EnterPage()
        {
            Add<Modules.ShopUserUserForm>();
        }
    }
}