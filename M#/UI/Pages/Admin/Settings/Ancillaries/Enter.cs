using MSharp;

namespace Admin.Settings.Ancillaries
{
    class EnterPage : SubPage<Admin.SettingsPage>
    {
        public EnterPage()
        {
            Add<Modules.AncillaryForm>();
        }
    }
}