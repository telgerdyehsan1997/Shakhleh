using MSharp;

namespace Admin.Company.ContactGroup
{
    class EnterPage : SubPage<ContactGroupsPage>
    {
        public EnterPage()
        {
            Add<Modules.ContactGroupForm>();
        }
    }
}