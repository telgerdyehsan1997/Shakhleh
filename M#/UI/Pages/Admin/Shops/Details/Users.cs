using MSharp;
using Domain;

namespace Admin.Shops
{
    public class UsersPage : SubPage<ShopsPage>
    {
        public UsersPage()
        {
            Set(PageSettings.LeftMenu, "AdminShopsMenu");

            Add<Modules.UserList>();
        }
    }
}