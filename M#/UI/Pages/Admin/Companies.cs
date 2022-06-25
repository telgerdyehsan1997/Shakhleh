using MSharp;

namespace Admin
{
    class CompaniesPage : SubPage<AdminPage>
    {
        public CompaniesPage()
        {
            Add<Modules.CompanyList>();
        }
    }
}