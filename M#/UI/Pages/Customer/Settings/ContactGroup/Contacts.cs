using MSharp;

namespace Customer.Settings.ContactGroup
{
    class ContactsPage : SubPage<CustomerContactGroupsPage>
    {
        public ContactsPage()
        {
            Add<Modules.CustomerContactGroupContacts>();
        }
    }
}