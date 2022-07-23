using MSharp;
using Domain;

namespace Admin.Shops.Details.Users
{
    public class EnterPage : SubPage<UsersPage>
    {
        public EnterPage()
        {
            Add<Modules.UserForm>();
        }
    }
}