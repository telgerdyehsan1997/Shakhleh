using MSharp;

namespace Admin.Company.Contact
{
    class EnterPage : SubPage<ContactsPage>
    {
        public EnterPage()
        {
            Add<Modules.ContactDetails>();
        }
    }
}