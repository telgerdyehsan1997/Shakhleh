using MSharp;

namespace Admin.Company
{
    class DetailsPage : SubPage<CompaniesPage>
    {
        public DetailsPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.CompanyView>();
        }
    }
}