using MSharp;

namespace Admin.Settings
{
    class UNCodeImportsPage : SubPage<SettingsPage>
    {
        public UNCodeImportsPage()
        {
            Add<Modules.ImportUNCodesList>();
        }
    }
}
