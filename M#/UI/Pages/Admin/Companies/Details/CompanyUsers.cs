using MSharp;

namespace Admin.Company
{
    class CompanyUsersPage : SubPage<CompaniesPage>
    {
        public CompanyUsersPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.CompanyUsersList>();
        }
    }
}
