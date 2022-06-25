using MSharp;

namespace Admin.Company.ContactGroup
{
    class ContactsPage : SubPage<ContactGroupsPage>
    {
        public ContactsPage()
        {
            Add<Modules.ContactGroupContacts>();
        }
    }
}