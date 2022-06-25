using MSharp;

namespace Customer.Settings.Contact
{
    class EnterPage : SubPage<CustomerContactsPage>
    {
        public EnterPage()
        {
            Add<Modules.CustomerContactForm>();
        }
    }
}