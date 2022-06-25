using MSharp;

namespace Admin.Company
{
    class APISettingsPage : SubPage<CompaniesPage>
    {
        public APISettingsPage()
        {
            Roles(AppRole.Admin);
            Set(PageSettings.LeftMenu, "CompanyMenu");
            Add<Modules.CompanyAPISettingsForm>();
        }
    }
}
