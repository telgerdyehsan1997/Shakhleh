using MSharp;

namespace Admin.Company
{
    class ContactsPage : SubPage<CompaniesPage>
    {
        public ContactsPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.ContactList>();
        }
    }
}