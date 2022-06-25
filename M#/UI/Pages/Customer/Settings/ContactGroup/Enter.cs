using MSharp;

namespace Customer.Settings.ContactGroup
{
    class EnterPage : SubPage<CustomerContactGroupsPage>
    {
        public EnterPage()
        {
            Add<Modules.CustomerContactGroupForm>();
        }
    }
}