using MSharp;

namespace Admin.Company
{
    class ContactGroupsPage : SubPage<CompaniesPage>
    {
        public ContactGroupsPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.ContactGroupList>();
        }
    }
}