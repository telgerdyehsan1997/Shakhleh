using MSharp;

namespace Admin.Company
{
    class AncillariesPage : SubPage<CompaniesPage>
    {
        public AncillariesPage()
        {
            Roles(AppRole.SuperAdmin);

            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.CompanyAncillaries>();
        }
    }
}