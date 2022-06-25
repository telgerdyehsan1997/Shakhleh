using MSharp;

namespace Admin.Company.CompanyUser
{
    class EnterPage : SubPage<CompanyUsersPage>
    {
        public EnterPage()
        {
            Add<Modules.CompanyUserDetails>();
        }
    }
}
