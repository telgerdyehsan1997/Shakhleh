using MSharp;

namespace Admin.Company
{
    class EnterPage : SubPage<CompaniesPage>
    {
        public EnterPage()
        {
            Add<Modules.CompanyForm>();
        }
    }
}