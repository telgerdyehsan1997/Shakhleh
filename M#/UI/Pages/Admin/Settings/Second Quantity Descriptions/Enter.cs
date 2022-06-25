using MSharp;

namespace Admin.Settings.SecondQuantityDescriptions
{
    class EnterPage : SubPage<Admin.Settings.SecondQuantityDescriptionsPage>
    {
        public EnterPage()
        {
            Add<Modules.SecondQuantityDescriptionsForm>();
        }
    }
}
