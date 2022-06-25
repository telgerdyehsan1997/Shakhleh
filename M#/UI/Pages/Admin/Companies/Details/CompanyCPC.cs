using MSharp;

namespace Admin.Company
{
    class CompanyCPCPage : SubPage<CompaniesPage>
    {
        public CompanyCPCPage()
        {

            Set(PageSettings.LeftMenu, "CompanyMenu");
            Add<Modules.CompanyCPCList>();
        }
    }
}